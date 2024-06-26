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

    public void SetupCard(int lv,int ownedStars,bool isUnlocked)
    {
        levelText.text = lv.ToString();
        for (int i = 0; i < ownedStars; i++)
        {
            try
            {
                starHolders[i].sprite = starImg;
            }
            catch
            {
                starHolders[i].sprite = null;
            }
        }
        GetComponent<Button>().onClick.AddListener(() => Actions.SelectedLevel?.Invoke(lv));
        GetComponent<Button>().interactable = isUnlocked;
    }
}
