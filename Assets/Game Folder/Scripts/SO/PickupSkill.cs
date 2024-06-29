using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Pickup",menuName = "Skill/Pickup")]
public class PickupSkill : Skill
{
    public override IEnumerator UseSkill()
    {
        if (Player.GetComponent<PlayerInventory>() == null)
            Player.AddComponent<PlayerInventory>();
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.CompareTag("Trash"))
                    {
                        Actions.AddObjectToinventory?.Invoke(hit.collider.gameObject);
                    }
                    if (hit.collider.CompareTag("Trashcan"))
                    {
                        Actions.ThrowGarbage?.Invoke();
                    }
                }
            }
        }
    }
}
