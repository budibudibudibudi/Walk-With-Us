using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPage : Page
{
    [SerializeField] private Button playBTN,settingBTN,levelBTN,exitBTN;
    protected override void Start()
    {
        AudioManager.instance.StopAllMusic();
        AudioManager.instance.PlayMusic("Menu");
        playBTN.onClick.AddListener(() => Actions.SelectedLevel?.Invoke(1));
        settingBTN.onClick.AddListener(() => Actions.OnPageChange?.Invoke(PAGENAME.OPTIONSPAGE));
        levelBTN.onClick.AddListener(() => Actions.OnPageChange?.Invoke(PAGENAME.LEVELPAGE));
        exitBTN.onClick.AddListener(()=>Application.Quit());
    }
}
