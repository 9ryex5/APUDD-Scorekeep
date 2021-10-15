using System;
using System.Collections.Generic;

public enum Language { EN, PT }

[Serializable]
public struct Team
{
    public string myName;
    public List<Player> players;
}

[Serializable]
public struct Player
{
    public int ID;
    public string myName;
    public int number;
    public bool female;
}

[Serializable]
public struct Match
{
    public Team teamA, teamB;
    public List<MatchEvent> events;

    public Match(Team _a, Team _b)
    {
        teamA = _a;
        teamB = _b;
        events = new List<MatchEvent>();
    }
}

[Serializable]
public struct MatchEvent
{
    public TimeSpan time;
    public MatchEventType eventType;
    public bool teamA;
    public Player eventPlayer;
    public Player playerAssist;
}

public enum MatchEventType
{
    POINT,
    DEFENSE,
    CALLAHAN,
    TIMEOUT,
}