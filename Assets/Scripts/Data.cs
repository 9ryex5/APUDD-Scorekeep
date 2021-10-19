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
    public DateTime date;
    public TimeSpan halfTime, fullTime;
    public Team teamA, teamB;
    public List<MatchEvent> events;

    public Match(DateTime _date, TimeSpan _ht, TimeSpan _ft, Team _a, Team _b)
    {
        date = _date;
        halfTime = _ht;
        fullTime = _ft;
        teamA = _a;
        teamB = _b;
        events = new List<MatchEvent>();
    }

    public int GetScore(bool _teamA)
    {
        int sum = 0;
        for (int i = 0; i < events.Count; i++)
            if ((events[i].eventType == MatchEventType.POINT || events[i].eventType == MatchEventType.CALLAHAN) && events[i].teamA == _teamA) sum++;

        return sum;
    }
}

[Serializable]
public struct MatchEvent
{
    public TimeSpan gameTime;
    public MatchEventType eventType;
    public bool teamA;
    public Player playerMain;
    public Player playerAssist;
}

public enum MatchEventType
{
    POINT,
    DEFENSE,
    CALLAHAN,
    TIMEOUT,
}