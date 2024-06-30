using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GamePage : Page
{
    [SerializeField] private TMP_Text titleLevel,inventText,warningText;

    [SerializeField] private QuestCard questCardPrefab;
    [SerializeField] private Transform questContainer;

    protected override void Start()
    {
        SetupQuest();
        RefreshInventory();
    }
    private void OnEnable()
    {
        Actions.ShowWarningText += ShowWarnText;
        Actions.RefreshInventory += RefreshInventory;
    }

    private void OnDisable()
    {
        Actions.ShowWarningText -= ShowWarnText;
        Actions.RefreshInventory -= RefreshInventory;
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
