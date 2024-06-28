using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private LevelData[] levelDatas;

    public LevelData[] GetLevelDatas() { return levelDatas; }

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
        foreach (var item in levelData.listQuest)
        {
            if (item.Complete())
            {
                levelData.completedStar++;
            }
        }
        return levelData.completedStar;
    }
}
