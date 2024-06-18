using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsPage : Page
{
    [SerializeField] private Slider musicSlider, sfxSlider;
    [SerializeField] Button backBTN;
    protected override void Start()
    {
        base.Start();
        musicSlider.onValueChanged?.AddListener((value)=>AudioManager.instance.SetVolume(Audio.AudioType.music, value));
        sfxSlider.onValueChanged?.AddListener((value)=>AudioManager.instance.SetVolume(Audio.AudioType.soundEffect, value));
        backBTN.onClick.AddListener(() => Actions.OnPageChange?.Invoke(PAGENAME.MENUPAGE));
    }
}
