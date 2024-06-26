using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance;
    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    #endregion
    [SerializeField] private GAMESTATE currentState;
    private GAMESTATE GetGamestate()
    {
        return currentState;
    }
    private LevelManager levelManager;

    private LevelManager GetLevelManager()
    {
        if (levelManager == null)
        {
            levelManager = GetComponentInChildren<LevelManager>();
        }
        return levelManager;
    }
    [SerializeField] private int currentLevel;
    [SerializeField] private float Timer;
    private void OnEnable()
    {
        Actions.OnStateChange += OnStateChange;
        Funcs.GetAllSkillCurrentLevel += GetListSkillCurrentLevel;
        Funcs.GetCurrentLevel += GetCurrentLevel;
        Funcs.GetTimer += GetTimer;
        Funcs.GetGameState += GetGamestate;
        Funcs.GetLevelManager += GetLevelManager;
        Actions.AddSkillToPlayer += SaveSkill;
        Actions.SelectedLevel += GotoLevel;
    }
    private void OnDisable()
    {
        Actions.OnStateChange -= OnStateChange;
        Funcs.GetAllSkillCurrentLevel -= GetListSkillCurrentLevel;
        Funcs.GetCurrentLevel -= GetCurrentLevel;
        Funcs.GetTimer -= GetTimer;
        Funcs.GetGameState -= GetGamestate;
        Actions.AddSkillToPlayer -= SaveSkill;
        Actions.SelectedLevel -= GotoLevel;
    }

    private float GetTimer()
    {
        return Timer;
    }
    private void SaveSkill(Skill obj)
    {
        List<string> listskill = JsonHelper.ReadListFromJSON<string>("Player Skill List");
        if (listskill == null)
        {
            listskill = new();
            listskill.Add(obj.skillName);
            JsonHelper.SaveToJSON(listskill, "Player Skill List");
        }
        else
        {
            foreach (var item in listskill)
            {
                if (item != obj.skillName)
                {
                    listskill.Add(obj.skillName);
                    JsonHelper.SaveToJSON(listskill, "Player Skill List");
                }
            }
        }
    }

    private int GetCurrentLevel()
    {
        return currentLevel;
    }
    private Skill[] GetListSkillCurrentLevel()
    {
        return levelManager.GetLevelDatas()[currentLevel].listSkill;
    }

    private void OnStateChange(GAMESTATE gAMESTATE)
    {
        switch (gAMESTATE)
        {
            case GAMESTATE.PLAY:
                Timer = 0;
                StartCoroutine(StartTimer());
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                Actions.OnPageChange?.Invoke(PAGENAME.GAMEPAGE);

                break;
            case GAMESTATE.PAUSE:
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                break;
            case GAMESTATE.BUFFING:
                Cursor.lockState = CursorLockMode.None;
                bool isPlayerHasSkill = CheckSkillPlayer(levelManager.GetLevelDatas()[currentLevel].listSkill);
                if (!isPlayerHasSkill)
                {
                    Time.timeScale = 0;
                    Actions.OnPageChange?.Invoke(PAGENAME.BUFFCARDSPAGE);
                }
                else
                {
                    foreach (var item in levelManager.GetLevelDatas()[currentLevel].listSkill)
                    {
                        Actions.AddSkillToPlayer?.Invoke(item);
                    }
                    Actions.OnStateChange?.Invoke(GAMESTATE.PLAY);
                }
                break;
            case GAMESTATE.GAMEOVER:
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0;
                Actions.OnPageChange?.Invoke(PAGENAME.FINISHPAGE);
                break;
            case GAMESTATE.NEXTLEVEL:
                currentLevel++;
                SceneManager.LoadScene($"LV{currentLevel + 1}");
                SceneManager.sceneLoaded += (scene, loaded) =>
                {
                    switch (loaded)
                    {
                        case LoadSceneMode.Single:
                            Actions.OnStateChange?.Invoke(GAMESTATE.BUFFING);
                            break;
                        case LoadSceneMode.Additive:
                            break;
                        default:
                            break;
                    }
                };
                SceneManager.sceneLoaded -= (scene, loaded) =>
                {
                    switch (loaded)
                    {
                        case LoadSceneMode.Single:
                            Actions.OnStateChange?.Invoke(GAMESTATE.BUFFING);
                            break;
                        case LoadSceneMode.Additive:
                            break;
                        default:
                            break;
                    }
                };
                break;
            case GAMESTATE.MENU:
                Cursor.lockState = CursorLockMode.None;
                break;
        }
    }

    private IEnumerator StartTimer()
    {
        while (currentState == GAMESTATE.PLAY)
        {
            Timer += Time.deltaTime;
            Actions.OnTimerRun?.Invoke(Timer);
            yield return null;
        }
    }


    private void GotoLevel(int value)
    {
        currentLevel = value;
        SceneManager.LoadScene($"LV{currentLevel}");
    }

    private bool CheckSkillPlayer(Skill[] skills)
    {
        List<string> listskill = JsonHelper.ReadListFromJSON<string>("Player Skill List");
        if (listskill == null)
            return false;
        if (listskill.Count <= 0)
        {
            return false;
        }
        else
        {
            foreach (var item in listskill)
            {
                foreach (var skill in skills)
                {
                    if (item == skill.skillName)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
}
