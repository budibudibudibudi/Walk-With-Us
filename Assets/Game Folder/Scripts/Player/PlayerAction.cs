using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [SerializeField] private List<Skill> listSkillOfPlayer;

    private void OnEnable()
    {
        Actions.AddSkillToPlayer += AddSkill;
        Actions.OnStateChange += OnStateChange;
    }

    private void OnDisable()
    {
        Actions.AddSkillToPlayer -= AddSkill;
        Actions.OnStateChange -= OnStateChange;
    }
    private void OnStateChange(GAMESTATE gAMESTATE)
    {
        switch (gAMESTATE)
        {
            case GAMESTATE.PLAY:
                foreach (var item in listSkillOfPlayer)
                {
                    item.Activate(gameObject);
                    StartCoroutine(item.UseSkill());
                }
                break;
            case GAMESTATE.PAUSE:
                break;
            case GAMESTATE.BUFFING:
                break;
            default:
                break;
        }
    }

    private void AddSkill(Skill skill)
    {
        listSkillOfPlayer.Add(skill);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Finish")
        {
            Actions.OnStateChange?.Invoke(GAMESTATE.GAMEOVER);
        }
    }
}
