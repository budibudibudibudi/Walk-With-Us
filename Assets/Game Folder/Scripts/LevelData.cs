using System;
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

}
