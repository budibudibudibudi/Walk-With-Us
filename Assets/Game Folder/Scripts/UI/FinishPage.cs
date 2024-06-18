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
        int currentLevel = Funcs.GetCurrentLevel.Invoke();
        nextBTN.onClick.AddListener(() => Actions.OnStateChange?.Invoke(GAMESTATE.NEXTLEVEL));
        levelBTN.onClick.AddListener(() => Actions.OnPageChange?.Invoke(PAGENAME.LEVELPAGE));
        retryBTN.onClick.AddListener(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));
        timerText.text = Funcs.GetTimer().ToString("F2");

        int star = Funcs.GetLevelDatas()[Funcs.GetCurrentLevel()].completedStar;

        for (int i = 0; i < star; i++)
        {
            try
            {
                starsHolder[i].sprite = starSprite;

            }
            catch 
            {
                starsHolder[i].sprite = null;
            }
        }
    }
}
