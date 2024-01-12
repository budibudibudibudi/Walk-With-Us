using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : ScriptableObject
{
    public string skillName;
    public Sprite Icon;
    public string Description;

    protected GameObject Player;
    public virtual IEnumerator UseSkill()
    {
        yield break;
    }
    public virtual void Activate(GameObject player)
    {
        Player = player;
    }
}
