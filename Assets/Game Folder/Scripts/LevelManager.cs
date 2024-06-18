using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private LevelData[] levelDatas;

    public LevelData[] GetLevelDatas() { return levelDatas; }

    private void OnEnable()
    {
        Funcs.GetLevelDatas += GetLevelDatas;
    }
}
