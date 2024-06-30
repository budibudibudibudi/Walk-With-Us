using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelCard : MonoBehaviour
{
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private Image[] starHolders;
    [SerializeField] private Sprite starImg;

    public void SetupCard(LevelData levelData)
    {
        levelText.text = levelData.level.ToString();
        for (int i = 0; i < levelData.completedStar; i++)
        {
            try
            {
                starHolders[i].sprite = starImg;
            }
            catch
            {
                continue;
            }
        }
        GetComponent<Button>().onClick.AddListener(() => Actions.SelectedLevel?.Invoke(levelData.level));
        GetComponent<Button>().interactable = levelData.isUnlocked;
    }
}
