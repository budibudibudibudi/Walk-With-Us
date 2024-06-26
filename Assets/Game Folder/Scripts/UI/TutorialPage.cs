using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPage : Page
{
    [SerializeField] private Button closeBTN;
    protected override void Start()
    {
        closeBTN.onClick.AddListener(() => Actions.OnStateChange?.Invoke(GAMESTATE.BUFFING));
    }
}
