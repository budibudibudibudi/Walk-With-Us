using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsPage : Page
{
    [SerializeField] private Slider musicSlider, sfxSlider;
    [SerializeField] Button backBTN,creditBTN;
    protected override void Start()
    {
        base.Start();
        musicSlider.value = AudioManager.instance.musicVolume;
        sfxSlider.value = AudioManager.instance.sfxVolume;
        musicSlider.onValueChanged?.AddListener((value) => AudioManager.instance.SetVolume(Audio.AudioType.music, value));
        sfxSlider.onValueChanged?.AddListener((value) => AudioManager.instance.SetVolume(Audio.AudioType.soundEffect, value));
        backBTN.onClick.AddListener(() => Actions.OnStateChange?.Invoke(GAMESTATE.UNPAUSE));
    
        creditBTN.onClick.AddListener(() => Actions.OnPageChange?.Invoke(PAGENAME.CREDITPAGE));
    }
}
