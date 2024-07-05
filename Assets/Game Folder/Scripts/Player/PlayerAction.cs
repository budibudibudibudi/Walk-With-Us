using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [SerializeField] private List<Skill> listSkillOfPlayer;
    [SerializeField] private int waterHit;
    [SerializeField] private int Key;
    [SerializeField] private int zebraCross;
    private void Start()
    {
        List<string> listskill = JsonHelper.ReadListFromJSON<string>("Player Skill List");
        if (listskill != null)
        {
            foreach (var item in listskill)
            {
                Skill newSkill = Array.Find(Funcs.GetAllSkill(), n => n.skillName == item);
                if(newSkill != null)
                    listSkillOfPlayer.Add(newSkill);
            }
        }
    }
    private void OnEnable()
    {
        Actions.AddSkillToPlayer += AddSkill;
        Actions.OnStateChange += OnStateChange;
        Actions.SaveSkillsPlayer += SaveListSkill;
        Funcs.GetHitWater += GetWaterhit;
        Funcs.GetKeyCount += GetKey;
        Funcs.GetZebraCrossWalk += GetZebraCross;
    }

    private void OnDisable()
    {
        Actions.AddSkillToPlayer -= AddSkill;
        Actions.OnStateChange -= OnStateChange;
        Actions.SaveSkillsPlayer -= SaveListSkill;
        Funcs.GetHitWater -= GetWaterhit;
        Funcs.GetKeyCount -= GetKey;
        Funcs.GetZebraCrossWalk -= GetZebraCross;
    }

    private int GetZebraCross()
    {
        return zebraCross;
    }

    private int GetKey()
    {
        return Key;
    }

    private int GetWaterhit()
    {
        return waterHit;
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
            case GAMESTATE.GAMEOVER:
                foreach (var item in listSkillOfPlayer)
                {
                    StopCoroutine(item.UseSkill());
                }
                break;
            default:
                break;
        }
    }

    private void SaveListSkill()
    {
        List<string> listskill = new();
        foreach (var item in listSkillOfPlayer)
        {
            listskill.Add(item.skillName);
        }
        JsonHelper.SaveToJSON(listskill, "Player Skill List");
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
        if (collision.gameObject.tag == "Key")
        {
            Key++;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "ZebraCross")
        {
            zebraCross++;
        }
        if (collision.gameObject.tag == "Bush")
        {
            KnockBack();
        }
    }

    private async void KnockBack()
    {
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<Rigidbody>().velocity = (-transform.forward) * 5;
        await Task.Delay(1000);
        GetComponent<PlayerMovement>().enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            waterHit++;
        }
        if (other.CompareTag("Waterkill"))
        {
            Actions.OnStateChange?.Invoke(GAMESTATE.LOSE);
            gameObject.SetActive(false);
        }
    }
}
