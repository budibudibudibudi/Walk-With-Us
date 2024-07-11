using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GamePage : Page
{
    [SerializeField] private TMP_Text titleLevel, inventText, warningText, timerText,trashText;

    [SerializeField] private QuestCard questCardPrefab;
    [SerializeField] private Transform questContainer;

    protected override void Start()
    {
        SetupQuest();
        RefreshInventory();
        if (Funcs.GetTrashAmountInScene() > 0)
        {
            OnTrashChange(Funcs.GetTrashAmountInScene());
        }
        else
        {
            trashText.text = "";
        }
    }
    private void OnEnable()
    {
        Actions.ShowWarningText += ShowWarnText;
        Actions.RefreshInventory += RefreshInventory;
        Actions.OnTimerRun += OnTimerRun;
        Actions.OnTrashChange += OnTrashChange;
    }

    private void OnDisable()
    {
        Actions.ShowWarningText -= ShowWarnText;
        Actions.RefreshInventory -= RefreshInventory;
        Actions.OnTimerRun -= OnTimerRun;
        Actions.OnTrashChange -= OnTrashChange;
    }

    private void OnTrashChange(int obj)
    {
        trashText.text = $"{obj} Trash";
    }

    private void OnTimerRun(float obj)
    {
        string minutes = ((int)obj / 60).ToString("00");
        string seconds = (obj % 60).ToString("00");
        timerText.text = minutes+":" + seconds;
    }
    private void RefreshInventory()
    {
        inventText.text = $"{Funcs.GetItemInInventory()[0]}/{Funcs.GetItemInInventory()[1]}";
    }
    private void ShowWarnText(string obj)
    {
        warningText.text = obj;
    }

    private void SetupQuest()
    {
        LevelData target = Array.Find(Funcs.GetLevelDatas(), t => t.level == Funcs.GetCurrentLevel());
        foreach (var item in target.listQuest)
        {
            if (item.listSubQuest[0].questID != QuestID.NONE)
            {
                QuestCard questCard = Instantiate(questCardPrefab, questContainer).GetComponent<QuestCard>();
                questCard.SetupCard(item.name);
            }
        }
        titleLevel.text = target.description;
    }
}
