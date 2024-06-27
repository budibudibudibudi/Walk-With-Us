using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Quest",menuName ="Quest")]
public class Quest:ScriptableObject
{
    public SubQuest[] listSubQuest;
    public bool allSubquestComplete;

    public virtual void Init()
    {
        Actions.ReportQuest += OnReportedQuest;
    }

    protected virtual void OnReportedQuest(QuestID iD,int value)
    {
        int temp = 0;
        foreach (var subQuest in listSubQuest)
        {
            if (subQuest.questID == iD)
            {
                switch (iD)
                {
                    case QuestID.Timer:
                        if (subQuest.amount > value)
                        {
                            subQuest.Complete = true;
                        }
                        break;
                    case QuestID.FindObject:
                        if (value >= subQuest.amount)
                        {
                            subQuest.Complete = true;
                        }
                        break;
                    case QuestID.ClearTile:
                        if (value == subQuest.amount)
                        {
                            subQuest.Complete = true;
                        }
                        break;
                    case QuestID.HitWater:
                        if (value < subQuest.amount)
                        {
                            subQuest.Complete = true;
                        }
                        break;
                    case QuestID.NotWalkToMidRoad:
                        subQuest.Complete = true;
                        break;
                    case QuestID.Fall:
                        if (value < subQuest.amount)
                        {
                            subQuest.Complete = true;
                        }
                        break;
                    default:
                        break;
                }
            }
            if (subQuest.Complete) temp++;
        }
        if (listSubQuest.Length == temp)
        {
            allSubquestComplete = true;
        }
    }
}
[System.Serializable]
public class SubQuest
{
    public QuestID questID;
    public int amount;
    public bool Complete;

}
