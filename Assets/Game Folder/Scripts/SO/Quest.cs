using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Quest",menuName ="Quest")]
public class Quest:ScriptableObject
{
    public SubQuest[] listSubQuest;
    public bool Complete()
    {
        int temp = 0;
        foreach (SubQuest subQuest in listSubQuest)
        {
            if (subQuest.Complete)
            {
                temp++;
            }
        }
        if (temp == listSubQuest.Length)
        {
            return true;
        }
        return false;   
    }

    public virtual void Init()
    {
        Actions.ReportQuest += OnReportedQuest;
    }

    protected virtual void OnReportedQuest(QuestID iD,int value)
    {
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
                        if (value <= subQuest.amount)
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
                    case QuestID.ZebraCross:
                        if (value >= subQuest.amount)
                        {
                            subQuest.Complete = true;
                        }
                        break;
                    default:
                        break;
                }
            }
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
