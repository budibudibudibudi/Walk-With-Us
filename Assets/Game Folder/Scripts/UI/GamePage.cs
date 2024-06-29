using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GamePage : Page
{
    [SerializeField] private TMP_Text titleLevel;

    [SerializeField] private QuestCard questCardPrefab;
    [SerializeField] private Transform questContainer;

    protected override void Start()
    {
        SetupQuest();
    }

    private void SetupQuest()
    {
        foreach (var item in Funcs.GetLevelDatas()[Funcs.GetCurrentLevel()].listQuest)
        {
            QuestCard questCard = Instantiate(questCardPrefab, questContainer).GetComponent<QuestCard>();
            questCard.SetupCard(item.name);
        }
        titleLevel.text = Funcs.GetLevelDatas()[Funcs.GetCurrentLevel()].description;
    }
}
