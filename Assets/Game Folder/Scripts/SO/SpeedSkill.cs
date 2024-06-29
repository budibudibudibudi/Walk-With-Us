using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="AddSpeed+0,3",menuName ="Skill/AddSpeed0,3")]
public class SpeedSkill : Skill
{
    public float AddPower = .3f;
    public override IEnumerator UseSkill()
    {
        Player.GetComponent<PlayerMovement>().AddSpeed(AddPower);
        yield return null;
    }
}
