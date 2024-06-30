using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCanvasManager : CanvasManager
{
    private void OnEnable()
    {
        Actions.OnPageChange += OnPageChange;
    }
    private void OnDisable()
    {
        Actions.OnPageChange -= OnPageChange;
    }

    private void OnPageChange(PAGENAME pageName)
    {
        SetPage(pageName);
    }
}
