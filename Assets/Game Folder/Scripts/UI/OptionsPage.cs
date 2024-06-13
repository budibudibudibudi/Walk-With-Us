using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsPage : Page
{
    [SerializeField] private Toggle masterToogle, musicToogle, sfxToogle;
    [SerializeField] private Button backBTN;
    private void Start()
    {
        backBTN.onClick.AddListener(() => Actions.OnPageChange?.Invoke(PAGENAME.MENUPAGE));
    }
}
