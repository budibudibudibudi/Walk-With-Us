using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Jump", menuName = "Skill/Jump")]
public class JumpSkill : Skill
{
    public float addPower;
    public override IEnumerator UseSkill()
    {
        if (!Player.GetComponent<PlayerMovement>().canJump)
        {
            Player.GetComponent<PlayerMovement>().canJump = true;
        }
        else
        {
            Player.GetComponent<PlayerMovement>().AddJumpForce(addPower);
        }
        yield return null;
    }
}
