using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishPage : Page
{
    [SerializeField] private Button nextBTN,levelBTN,retryBTN;
    [SerializeField] private TMP_Text timerText,gameOverText;
    [SerializeField] private Image[] starsHolder;
    [SerializeField] private Sprite starSprite;

    protected override void Start()
    {
        int star = Funcs.GetCompletedStar();
        int currentLevel = Funcs.GetCurrentLevel.Invoke();
        nextBTN.onClick.AddListener(() => {
            LevelData levelData = Array.Find(Funcs.GetLevelDatas(),l=>l.level == currentLevel);
            if (levelData.isClear)
            {
                Actions.OnStateChange?.Invoke(GAMESTATE.NEXTLEVEL);
            }
            });
        levelBTN.onClick.AddListener(() => Actions.OnPageChange?.Invoke(PAGENAME.LEVELPAGE));
        retryBTN.onClick.AddListener(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));
        timerText.text = Funcs.GetTimer().ToString("F2");


        for (int i = 0; i < star; i++)
        {
            try
            {
                starsHolder[i].sprite = starSprite;

            }
            catch 
            {
                continue;
            }
        }
    }
}
