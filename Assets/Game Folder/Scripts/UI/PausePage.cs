using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausePage : Page
{
    [SerializeField] private Button resumeBTN, optionsBTN, exitBTN;

    protected override void Start()
    {
        resumeBTN.onClick.AddListener(() => Actions.OnStateChange?.Invoke(GAMESTATE.PLAY));
        optionsBTN.onClick.AddListener(()=>Actions.OnPageChange?.Invoke(PAGENAME.OPTIONSPAGE));
        exitBTN.onClick.AddListener(() => Actions.OnStateChange?.Invoke(GAMESTATE.BACKTOMENU));
    }
}
