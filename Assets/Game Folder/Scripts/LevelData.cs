using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public int level;
    [TextArea(1, 5)]
    public string description;
    public bool isClear;
    public bool isUnlocked;
    public int completedStar;
    public Quest[] listQuest;
    public Skill[] listSkill;

    public void ResetQuest()
    {
        foreach (var item in listQuest)
        {
            foreach (var subQuest in item.listSubQuest)
            {
                if (subQuest.questID != QuestID.NONE)
                {
                    subQuest.Complete = false;
                }
            }
        }
    }
}
