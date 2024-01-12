using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuffCardsPage : Page
{
    [SerializeField] private GameObject Card;
    [SerializeField] private Transform contentParent;

    private void OnEnable()
    {
        if (contentParent.childCount > 0)
        {
            foreach (Transform item in contentParent)
            {
                Destroy(item.gameObject);
            }
        }
        Skill[] listCurrentBuff = Funcs.GetAllSkillCurrentLevel?.Invoke();

        foreach (var item in listCurrentBuff)
        {
            GameObject go = Instantiate(Card, contentParent);
            string _title = item.skillName;
            string _desc = item.Description;
            Sprite body = item.Icon;
            UnityAction btnAction = () => { Actions.AddSkillToPlayer?.Invoke(item); Actions.OnStateChange?.Invoke(GAMESTATE.PLAY); };
            go.GetComponent<BuffCard>().InitCard(_title, body, _desc, btnAction);
        }
    }
}
