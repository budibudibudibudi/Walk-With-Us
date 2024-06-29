using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestCard : MonoBehaviour
{
    [SerializeField] private TMP_Text questDescription;
    
    public void SetupCard(string desc)
    {
        questDescription.text = desc;
    }
}
