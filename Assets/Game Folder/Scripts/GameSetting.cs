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
        Actions.RefreshInventory += RefreshTrashAmount;
        Funcs.GetTrashAmountInScene += GetTrashAmount;
    }

    private void OnDisable()
    {
        Actions.RefreshInventory -= RefreshTrashAmount;
        Funcs.GetTrashAmountInScene -= GetTrashAmount;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Funcs.GetGameState() == GAMESTATE.GAMEOVER || Funcs.GetGameState() == GAMESTATE.WATERKILL) return;
            if (Funcs.GetGameState() != GAMESTATE.PAUSE)
            {
                Actions.OnStateChange?.Invoke(GAMESTATE.PAUSE);
            }
            else
            {
                Actions.OnStateChange?.Invoke(GAMESTATE.UNPAUSE);
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
        Actions.OnTrashChange?.Invoke(trashAmount);
        Actions.ReportQuest?.Invoke(QuestID.ClearTile, trashAmount);
    }
}
