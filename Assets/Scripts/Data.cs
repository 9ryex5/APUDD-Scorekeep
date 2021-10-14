using System.Collections.Generic;

public enum Language { EN, PT }

public struct Team
{
    public string name;
    public List<Player> players;
}

public struct Player
{
    public int ID;
    public string name;
    public int number;
    public bool female;
}

public struct MatchEvent
{
    public float time;
    public MatchEventType eventType;
    public Player playerGoal;
    public Player playerAssist;
}

public enum MatchEventType
{
    GOAL,
    DEFENSE,
    TIMEOUT,
}