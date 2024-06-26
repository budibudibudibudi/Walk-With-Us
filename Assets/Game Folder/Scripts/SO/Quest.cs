using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Quest",menuName ="Quest")]
public class Quest:ScriptableObject
{
    [SerializeField] protected SubQuest[] listQuest;

    public virtual void Init()
    {
        Actions.ReportQuest += OnReportedQuest;
    }

    protected virtual void OnReportedQuest(QuestID iD,int value)
    {
        switch (iD)
        {
            case QuestID.Timer:
                break;
            case QuestID.FindObject:
                break;
            case QuestID.ClearTile:
                break;
            case QuestID.HitWater:
                break;
            case QuestID.WalkToMidRoad:
                break;
            case QuestID.Fall:
                break;
            default:
                break;
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
