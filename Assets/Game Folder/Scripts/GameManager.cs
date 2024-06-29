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
    [SerializeField] private int currentLevel;
    [SerializeField] private float Timer;
    private void OnEnable()
    {
        Actions.OnStateChange += OnStateChange;
        Funcs.GetAllSkillCurrentLevel += GetListSkillCurrentLevel;
        Funcs.GetCurrentLevel += GetCurrentLevel;
        Funcs.GetTimer += GetTimer;
        Funcs.GetGameState += GetGamestate;
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
        LevelData levelData = Array.Find(Funcs.GetLevelDatas(),l=>l.level == currentLevel);
        return levelData.listSkill;
    }

    private void OnStateChange(GAMESTATE gAMESTATE)
    {
        currentState = gAMESTATE;
        switch (currentState)
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
                bool isPlayerHasSkill = CheckSkillPlayer(GetListSkillCurrentLevel());
                if (!isPlayerHasSkill)
                {
                    Time.timeScale = 0;
                    Actions.OnPageChange?.Invoke(PAGENAME.BUFFCARDSPAGE);
                }
                else
                {
                    foreach (var item in GetListSkillCurrentLevel())
                    {
                        Actions.AddSkillToPlayer?.Invoke(item);
                    }
                    Actions.OnStateChange?.Invoke(GAMESTATE.PLAY);
                }
                break;
            case GAMESTATE.GAMEOVER:
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0;
                Actions.ReportQuest(QuestID.Timer, (int)Timer);
                StarCalculation();
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

    private void StarCalculation()
    {
        LevelData levelData = Array.Find(Funcs.GetLevelDatas(), l => l.level == currentLevel);
        int star = Funcs.GetCompletedStar();
        if (star > 0)
            levelData.isClear = true;

        SaveLevelData(levelData);
    }

    private void SaveLevelData(LevelData levelData)
    {
        List<SavedLevelData> listLevelData = LoadListLevelData();
        SavedLevelData currentLevelData = null;
        int nextLevel = currentLevel++;
        if (listLevelData == null || listLevelData.Count == 0)
        {
            currentLevelData = new SavedLevelData();
        }
        else
        {
            currentLevelData = Array.Find(listLevelData.ToArray(), c => c.level == levelData.level);
            if (currentLevelData == null)
            {
                currentLevelData = new();
            }
        }
        currentLevelData.level = levelData.level;
        currentLevelData.isClear = levelData.isClear;
        currentLevelData.isUnlocked = levelData.isUnlocked;
        currentLevelData.completedStar = levelData.completedStar;
        JsonHelper.SaveToJSON(currentLevelData, "LevelData");
        if (levelData.isClear)
        {
            LevelData nextLevelData = Array.Find(Funcs.GetLevelDatas(), n => n.level == nextLevel);
            if (nextLevelData != null)
            {
                Actions.unlockNextLevel(nextLevelData);
            }
        }
    }
    private List<SavedLevelData> LoadListLevelData()
    {
        List<SavedLevelData> listLevelData = JsonHelper.ReadListFromJSON<SavedLevelData>("ListLevelData");
        return listLevelData;
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
        if (currentLevel > 1)
        {
            Actions.OnPageChange?.Invoke(PAGENAME.BUFFCARDSPAGE);
        }
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
