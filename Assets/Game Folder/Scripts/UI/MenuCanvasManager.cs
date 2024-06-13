using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCanvasManager : CanvasManager
{
    private void OnEnable()
    {
        Actions.OnPageChange += OnChangePage;
    }
    private void OnDisable()
    {
        Actions.OnPageChange -= OnChangePage;

    }

    private void OnChangePage(PAGENAME pAGENAME)
    {
        SetPage(pAGENAME);
    }
}
