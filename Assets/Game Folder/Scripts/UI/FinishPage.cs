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
    [SerializeField] private TMP_Text timerText,gameOverText,warningText;
    [SerializeField] private GameObject warningParent;
    [SerializeField] private Image[] starsHolder;
    [SerializeField] private Sprite starSprite,blackStarSprite;

    protected override void Start()
    {
        int star = Funcs.GetCompletedStar();
        int currentLevel = Funcs.GetCurrentLevel.Invoke();


        if (Funcs.GetGameState() == GAMESTATE.WATERKILL)
        {
            nextBTN.gameObject.SetActive(false);
            star = 0;
        }

        if (star <=0)
        {
            Debug.Log("Finish");
            AudioManager.instance.PlayMusic("Failure");
            nextBTN.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Finish, dengan total bintang " + star);
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
            Actions.OnStateChange?.Invoke(GAMESTATE.BACKTOMENU);
        });
        retryBTN.onClick.AddListener(() => GameManager.Instance.GotoLevel(currentLevel));


        string minutes = ((int)Funcs.GetTimer() / 60).ToString("00");
        string seconds = (Funcs.GetTimer() % 60).ToString("00");
        timerText.text = minutes + ":" + seconds;
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
    private void OnEnable()
    {
        Actions.FlashWarning += ShowWarn;
    }
    private void OnDisable()
    {
        Actions.FlashWarning -= ShowWarn;
    }

    private void ShowWarn(string obj)
    {
        warningParent.SetActive(true);
        warningText.text = obj;
    }
}
