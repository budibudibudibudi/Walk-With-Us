using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDatabase : MonoBehaviour
{
    [SerializeField] private Skill[] listSkill;

    private void OnEnable()
    {
        Funcs.GetAllSkill += GetListSkill;
    }
    private void OnDisable()
    {
        Funcs.GetAllSkill -= GetListSkill;

    }

    private Skill[] GetListSkill()
    {
        return listSkill;
    }
}
