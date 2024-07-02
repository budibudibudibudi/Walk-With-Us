using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditPage : Page
{
    [SerializeField] private Button backBTN;

    protected override void Start()
    {
        backBTN.onClick.AddListener(() => Actions.OnPageChange?.Invoke(PAGENAME.OPTIONSPAGE));
    }
}
