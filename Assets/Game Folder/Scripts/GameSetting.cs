using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetting : MonoBehaviour
{
    [SerializeField] private int trashAmount = 0;

    private void Start()
    {
        RefreshTrashAmount();
    }
    private void OnEnable()
    {
        Actions.ThrowGarbage += RefreshTrashAmount;
    }
    private void OnDisable()
    {
        Actions.ThrowGarbage -= RefreshTrashAmount;
    }

    private void RefreshTrashAmount()
    {
        trashAmount = GameObject.FindGameObjectsWithTag("Trash").Length;
        Actions.ReportQuest?.Invoke(QuestID.ClearTile, trashAmount);
    }
}
