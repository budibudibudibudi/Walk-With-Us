using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Funcs
{
    public static Func<Skill[]> GetAllSkillCurrentLevel;
    public static Func<int> GetCurrentLevel;
    public static Func<LevelData[]> GetLevelDatas;
    public static Func<float> GetTimer;
    public static Func<LevelManager> GetLevelManager;
    public static Func<GAMESTATE> GetGameState;
    public static Func<int> GetTrashAmountInScene;
}
