using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BuffCard : MonoBehaviour
{
    [SerializeField] private TMP_Text titleText,descriptionText;
    [SerializeField] private Image bodySprite;
    [SerializeField] private Button choseBTN;
    public void InitCard(string _title, Sprite _bodySprite, string _desc, UnityAction btnAction)
    {
        titleText.text = _title;
        //bodySprite.sprite = _bodySprite;
        descriptionText.text = _desc;
        choseBTN.onClick.AddListener(btnAction);
    }
}
