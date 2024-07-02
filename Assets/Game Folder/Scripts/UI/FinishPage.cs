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
    [SerializeField] private Sprite starSprite,blackStarSprite;

    protected override void Start()
    {
        int star = Funcs.GetCompletedStar();
        int currentLevel = Funcs.GetCurrentLevel.Invoke();
        if (star <=0)
        {
            AudioManager.instance.PlayMusic("Failure");
            nextBTN.gameObject.SetActive(false);
        }
        else
        {
            AudioManager.instance.PlayMusic("Finish");
            nextBTN.gameObject.SetActive(true);
        }

        nextBTN.onClick.AddListener(() => {
            LevelData levelData = Array.Find(Funcs.GetLevelDatas(),l=>l.level == currentLevel);
            if (levelData.isClear)
            {
                Actions.SaveSkillsPlayer?.Invoke();
                Actions.OnStateChange?.Invoke(GAMESTATE.NEXTLEVEL);
            }
            });
        levelBTN.onClick.AddListener(() =>
        {
            AudioManager.instance.StopAllMusic();
            SceneManager.LoadScene("MainMenu");
        });
        retryBTN.onClick.AddListener(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));


        timerText.text = Funcs.GetTimer().ToString("F2");
        gameOverText.text = Funcs.GetGameState() == GAMESTATE.GAMEOVER ? "You Win" : "You Lose";

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
