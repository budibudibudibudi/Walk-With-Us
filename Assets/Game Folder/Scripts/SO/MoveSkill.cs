using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Move", menuName = "Skill/Move")]
public class MoveSkill : Skill
{
    public override IEnumerator UseSkill()
    {
        Player.GetComponent<PlayerMovement>().canMove = true;
        yield return null;
    }
}
