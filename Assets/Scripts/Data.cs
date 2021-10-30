using UnityEngine;
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
    public string fullName;
    public int number;
    public bool female;

    public string GetName(bool _abreviation, int _index)
    {
        string[] fn = fullName.Split(' ');
        if (_index + 1 > fn.Length) return " ";
        string n = fn[_index];
        if (_abreviation && n.Length > 8) return n.Substring(0, 7) + '.';
        return n;
    }

    public string Identification()
    {
        return number + " " + fullName;
    }
}

[Serializable]
public class Match
{
    public string scoreKeepers;
    public DateTime date;
    public TimeSpan halfTime, fullTime;
    public Team teamA, teamB;
    public List<MatchEvent> events;
    public int[] spiritA, spiritB;

    public Match(DateTime _date, TimeSpan _ht, TimeSpan _ft, Team _a, Team _b)
    {
        scoreKeepers = string.Empty;
        date = _date;
        halfTime = _ht;
        fullTime = _ft;
        teamA = _a;
        teamB = _b;
        events = new List<MatchEvent>();
        spiritA = new int[5];
        spiritB = new int[5];
    }

    public int GetScore(bool _teamA, int _atIndex = -1)
    {
        int sum = 0;
        for (int i = 0; i < (_atIndex == -1 ? events.Count : _atIndex + 1); i++)
            if ((events[i].eventType == MatchEventType.POINT || events[i].eventType == MatchEventType.CALLAHAN) && events[i].teamA == _teamA) sum++;

        return sum;
    }

    public int GetScoreTotal()
    {
        return GetScore(false) + GetScore(true);
    }

    public int GetTotalSpirit(bool _teamA)
    {
        int sum = 0;

        for (int i = 0; i < spiritA.Length; i++)
            sum += _teamA ? spiritA[i] : spiritB[i];

        return sum;
    }
}

[Serializable]
public class MatchEvent
{
    public TimeSpan gameTime;
    public MatchEventType eventType;
    public bool teamA;
    public Player playerMain;
    public Player playerAssist;

    public string EventTypeString()
    {
        switch (eventType)
        {
            case MatchEventType.POINT:
                return ManagerLanguages.ML.Translate("Point");
            case MatchEventType.DEFENSE:
                return ManagerLanguages.ML.Translate("Defense");
            case MatchEventType.CALLAHAN:
                return ManagerLanguages.ML.Translate("Callahan");
            case MatchEventType.TIMEOUT:
                return ManagerLanguages.ML.Translate("Timeout");
            case MatchEventType.SPIRIT_TIMEOUT:
                return ManagerLanguages.ML.Translate("SpiritTimeout");
        }

        Debug.LogWarning("Invalid event type");
        return string.Empty;
    }
}

public enum MatchEventType
{
    POINT,
    DEFENSE,
    CALLAHAN,
    TIMEOUT,
    SPIRIT_TIMEOUT
}