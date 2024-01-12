using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Jump", menuName = "Skill/Jump")]
public class JumpSkill : Skill
{
    public override IEnumerator UseSkill()
    {
        Player.GetComponent<PlayerMovement>().canJump = true;
        yield return null;
    }
}
