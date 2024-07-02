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
        Funcs.GetTrashAmountInScene += GetTrashAmount;
    }

    private void OnDisable()
    {
        Actions.ThrowGarbage -= RefreshTrashAmount;
        Funcs.GetTrashAmountInScene -= GetTrashAmount;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Funcs.GetGameState() != GAMESTATE.PAUSE)
            {
                Actions.OnStateChange?.Invoke(GAMESTATE.PAUSE);
            }
            else
            {
                Actions.OnStateChange?.Invoke(GAMESTATE.PLAY);
            }
        }
    }
    private int GetTrashAmount()
    {
        return trashAmount;
    }

    private void RefreshTrashAmount()
    {
        trashAmount = GameObject.FindGameObjectsWithTag("Trash").Length;
        Actions.ReportQuest?.Invoke(QuestID.ClearTile, trashAmount);
    }
}
