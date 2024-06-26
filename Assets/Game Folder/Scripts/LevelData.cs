using System;

[System.Serializable]
public class LevelData
{
    public int level;
    public bool isClear;
    public bool isUnlocked;
    public int completedStar;
    public Quest[] listQuest;
    public Skill[] listSkill;

    public void CheckingQuest()
    {
    }
}
