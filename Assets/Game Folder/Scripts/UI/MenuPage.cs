using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPage : Page
{
    [SerializeField] private Button playBTN, optionBTN, exitBTN;

    private void Start()
    {
        playBTN.onClick.AddListener(() => SceneManager.LoadScene("LV1"));
        optionBTN.onClick.AddListener(()=>Actions.OnPageChange?.Invoke(PAGENAME.OPTIONSPAGE));
        exitBTN.onClick.AddListener(()=>Application.Quit());
    }
}
