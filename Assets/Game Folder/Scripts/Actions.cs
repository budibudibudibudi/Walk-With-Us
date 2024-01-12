using System;

public static class Actions
{
    public static Action<GAMESTATE> OnStateChange;
    public static Action<PAGENAME> OnPageChange;
    public static Action<Skill> AddSkillToPlayer;
}
