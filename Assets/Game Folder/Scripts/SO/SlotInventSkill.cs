using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="AddInventory",menuName ="Skill/Inventory")]
public class SlotInventSkill : Skill
{
    public int AddInventorySlot = 1;

    public override IEnumerator UseSkill()
    {
        Player.GetComponent<PlayerInventory>().AddInventorySlot(AddInventorySlot);
        yield return null;
    }
}
