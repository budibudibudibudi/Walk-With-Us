using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelPage : Page
{
    [SerializeField] private Transform contentParent;
    [SerializeField] private GameObject levelCardPrefab;

    [SerializeField] private Button backBTN;

    protected override void Start()
    {
        LevelData[] datas = Funcs.GetLevelDatas();
        foreach (var item in datas)
        {
            GameObject go = Instantiate(levelCardPrefab, contentParent);
            go.GetComponent<LevelCard>().SetupCard(item);
        }
        backBTN.onClick.AddListener(() => Actions.OnPageChange?.Invoke(PAGENAME.MENUPAGE));
    }
}
