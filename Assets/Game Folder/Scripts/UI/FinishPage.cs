using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishPage : Page
{
    [SerializeField] private Button nextBTN;

    private void Start()
    {
        int currentLevel = Funcs.GetCurrentLevel.Invoke();
        nextBTN.onClick.AddListener(() => SceneManager.LoadScene($"LV{currentLevel+1}"));
    }
}
