using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetting : MonoBehaviour
{
    [SerializeField] private GAMESTATE currentState;
    [SerializeField] private Level[] levelManager;
    [SerializeField] private int currentLevel;

    private void Start()
    {
        OnStateChange(currentState) ;
    }
    private void OnEnable()
    {
        Actions.OnStateChange += OnStateChange;
        Funcs.GetAllSkillCurrentLevel += GetListSkillCurrentLevel;
        Funcs.GetCurrentLevel += GetCurrentLevel;
    }

    private void OnDisable()
    {
        Actions.OnStateChange -= OnStateChange;
        Funcs.GetAllSkillCurrentLevel -= GetListSkillCurrentLevel;
        Funcs.GetCurrentLevel -= GetCurrentLevel;
    }

    private int GetCurrentLevel()
    {
        return currentLevel;
    }
    private Skill[] GetListSkillCurrentLevel()
    {
        return levelManager[currentLevel].listSkill;
    }

    private void OnStateChange(GAMESTATE gAMESTATE)
    {
        switch (gAMESTATE)
        {
            case GAMESTATE.PLAY:
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                Actions.OnPageChange?.Invoke(PAGENAME.GAMEPAGE);

                break;
            case GAMESTATE.PAUSE:
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                break;
            case GAMESTATE.BUFFING:
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0;
                Actions.OnPageChange?.Invoke(PAGENAME.BUFFCARDSPAGE);
                break;
            case GAMESTATE.GAMEOVER:
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0;
                Actions.OnPageChange?.Invoke(PAGENAME.FINISHPAGE);

                break;
        }
    }
}
