using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPage : Page
{
    [SerializeField] private Button playBTN,settingBTN,levelBTN;
    protected override void Start()
    {
        playBTN.onClick.AddListener(() => Actions.OnPageChange?.Invoke(PAGENAME.LEVELPAGE));
        settingBTN.onClick.AddListener(() => Actions.OnPageChange?.Invoke(PAGENAME.OPTIONSPAGE));
        levelBTN.onClick.AddListener(() => Actions.OnPageChange?.Invoke(PAGENAME.LEVELPAGE));
    }
}
