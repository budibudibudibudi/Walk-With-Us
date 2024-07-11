using System;
using UnityEngine;

public static class Actions
{
    public static Action<GAMESTATE> OnStateChange;
    public static Action<PAGENAME> OnPageChange;
    public static Action<Skill> AddSkillToPlayer;
    public static Action<float> OnTimerRun;
    public static Action<GameObject> AddObjectToinventory;
    public static Action ThrowGarbage;
    public static Action<QuestID,int> ReportQuest;
    public static Action<int> SelectedLevel;
    public static Action<LevelData> unlockNextLevel;
    public static Action<string> ShowWarningText;
    public static Action RefreshInventory;
    public static Action SaveSkillsPlayer;
    public static Action<int> OnTrashChange;
    public static Action<string> FlashWarning;
}
