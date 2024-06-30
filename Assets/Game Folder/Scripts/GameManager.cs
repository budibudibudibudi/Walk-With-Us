using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance;
    private void Awake()
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
        Actions.SelectedLevel += GotoLevel;
    }
    private void OnDisable()
    {
        Actions.OnStateChange -= OnStateChange;
        Funcs.GetAllSkillCurrentLevel -= GetListSkillCurrentLevel;
        Funcs.GetCurrentLevel -= GetCurrentLevel;
        Funcs.GetTimer -= GetTimer;
        Funcs.GetGameState -= GetGamestate;
        Actions.SelectedLevel -= GotoLevel;
    }

    private float GetTimer()
    {
        return Timer;
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
                AudioManager.instance.StopAllMusic();
                AudioManager.instance.PlayMusic("Game");
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
                bool openBuff = false;
                List<string> listskill = JsonHelper.ReadListFromJSON<string>("Player Skill List");
                if (listskill != null)
                {

                    foreach (var item in Funcs.GetAllSkillCurrentLevel())
                    {
                        if (item.Stackable)
                        {
                            openBuff = true;
                        }
                        else
                        {
                            if (!listskill.Contains(item.skillName))
                            {
                                openBuff = true;
                            }
                            else
                            {
                                openBuff = false;
                            }
                        }
                    }
                }
                else
                {

                    Actions.OnPageChange?.Invoke(PAGENAME.BUFFCARDSPAGE);
                    return;
                }
                if (openBuff)
                {
                    Actions.OnPageChange?.Invoke(PAGENAME.BUFFCARDSPAGE);
                }
                else
                {
                    Actions.OnStateChange?.Invoke(GAMESTATE.PLAY);
                }
                break;
            case GAMESTATE.GAMEOVER:
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0;
                Actions.ReportQuest(QuestID.Timer, (int)Timer); 
                Actions.ReportQuest(QuestID.HitWater, Funcs.GetHitWater()); 
                Actions.ReportQuest(QuestID.FindObject,Funcs.GetKeyCount());
                Actions.ReportQuest(QuestID.ZebraCross, Funcs.GetZebraCrossWalk()); 
                Actions.ReportQuest(QuestID.ClearTile, Funcs.GetTrashAmountInScene()); 
                StarCalculation();
                Actions.OnPageChange?.Invoke(PAGENAME.FINISHPAGE);
                break;
            case GAMESTATE.NEXTLEVEL:
                currentLevel++;
                GotoLevel(currentLevel);
                break;
            case GAMESTATE.MENU:
                Cursor.lockState = CursorLockMode.None;
                break;
            case GAMESTATE.LOSE:
                AudioManager.instance.PlayMusic("Failure");
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0;
                Funcs.GetLevelDatas()[currentLevel].ResetQuest();
                Actions.OnPageChange?.Invoke(PAGENAME.FINISHPAGE);
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
        if (listLevelData == null)
            listLevelData = new();
        SavedLevelData currentLevelData = null;
        int nextLevel = currentLevel+1;
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
        listLevelData.Add(currentLevelData);
        JsonHelper.SaveToJSON(listLevelData, "ListLevelData");
        if (levelData.isClear)
        {
            LevelData nextLevelData = Array.Find(Funcs.GetLevelDatas(), n => n.level == nextLevel);
            if (nextLevelData != null)
            {
                Actions.unlockNextLevel(nextLevelData);
            }
        }
    }
    public List<SavedLevelData> LoadListLevelData()
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


    private async void GotoLevel(int value)
    {
        currentLevel = value;
        AsyncOperation operation = SceneManager.LoadSceneAsync($"LV{currentLevel}");
        while (!operation.isDone)
        {
            if (!AudioManager.instance.CheckAudioIsPlaying("Loading"))
            {
                AudioManager.instance.PlayMusic("Loading");
            }
            await Task.Yield();
        }
        AudioManager.instance.StopMusic("Loading");
        if (currentLevel > 1)
        {
            Actions.OnStateChange?.Invoke(GAMESTATE.BUFFING);
        }
    }
}
