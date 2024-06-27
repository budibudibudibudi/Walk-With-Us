using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private Page[] listPage;

    private void Start()
    {
        listPage = GetComponentsInChildren<Page>(true);   
    }
    public void SetPage(PAGENAME pageName)
    {
        foreach (var item in listPage)
        {
            item.gameObject.SetActive(false);
        }

        Page currentPage = Array.Find(listPage, c => c.pageName == pageName);

        if (currentPage != null)
        {
            currentPage.gameObject.SetActive(true);
        }
    }
}
