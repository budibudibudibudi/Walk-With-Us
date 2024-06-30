using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Pickup",menuName = "Skill/Pickup")]
public class PickupSkill : Skill
{
    public Vector3 offset;
    public float radius;
    public override IEnumerator UseSkill()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Collider[] trash = Physics.OverlapSphere(Player.transform.position + offset, radius);
                if (trash.Length > 0)
                {
                    foreach (var item in trash)
                    {
                        if (item.CompareTag("Trash"))
                        {
                            Actions.AddObjectToinventory?.Invoke(item.gameObject);

                        }
                        if (item.CompareTag("Trashcan"))
                        {
                            Actions.ThrowGarbage?.Invoke();
                        }
                    }
                }
            }
            yield return null;
        }
    }
}
