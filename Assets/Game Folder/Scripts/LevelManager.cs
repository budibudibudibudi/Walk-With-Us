using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private LevelData[] levelDatas;

    public LevelData[] GetLevelDatas() { return levelDatas; }

    private void Start()
    {
        List<SavedLevelData> savedLevelDatas = GameManager.Instance.LoadListLevelData();
        if (savedLevelDatas == null) return;
        if (savedLevelDatas.Count > 0||savedLevelDatas != null)
        {
            foreach (var item in savedLevelDatas)
            {
                LevelData data = Array.Find(levelDatas, d => d.level == item.level);
                if (data != null)
                {
                    data.isClear = item.isClear;
                    data.isUnlocked = item.isUnlocked;
                    data.completedStar = item.completedStar;
                }
            }
        }
        for (int i = 0; i < levelDatas.Length; i++)
        {
            if (levelDatas[i].isClear)
            {
                int next = i + 1;
                try
                {
                    levelDatas[next].isUnlocked = true;

                }
                catch 
                {
                    break;
                }
            }
        }
    }
    private void OnEnable()
    {
        Funcs.GetLevelDatas += GetLevelDatas;
        Funcs.GetCompletedStar += GetCompletedStar;
        Actions.OnStateChange += OnStateChange;
        Actions.unlockNextLevel += UnlockNextLevel;
    }

    private void OnDisable()
    {
        Funcs.GetLevelDatas -= GetLevelDatas;
        Funcs.GetCompletedStar -= GetCompletedStar;
        Actions.OnStateChange -= OnStateChange;
        Actions.unlockNextLevel -= UnlockNextLevel;
    }

    private void UnlockNextLevel(LevelData data)
    {
        LevelData target = Array.Find(levelDatas,t=>t.level == data.level);
        target.isUnlocked = true;
    }

    private void OnStateChange(GAMESTATE gAMESTATE)
    {
        switch (gAMESTATE)
        {
            case GAMESTATE.PLAY:
                LevelData levelData = Array.Find(levelDatas,l=>l.level == Funcs.GetCurrentLevel());
                levelData.completedStar = 0;
                levelData.ResetQuest();
                foreach (var item in levelData.listQuest)
                {
                    item.Init();
                }
                break;
            case GAMESTATE.PAUSE:
                break;
            case GAMESTATE.BUFFING:
                break;
            case GAMESTATE.GAMEOVER:
                break;
            case GAMESTATE.NEXTLEVEL:
                break;
            case GAMESTATE.MENU:
                break;
            default:
                break;
        }
    }
    private int GetCompletedStar()
    {
        LevelData levelData = Array.Find(levelDatas, l => l.level == Funcs.GetCurrentLevel());
        levelData.completedStar = 0;
        foreach (var item in levelData.listQuest)
        {
            if (item.Complete())
            {
                levelData.completedStar++;
            }
        }
        int send = Mathf.Clamp(levelData.completedStar, 0, 3);
        return levelData.completedStar;
    }
}
