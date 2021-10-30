using UnityEngine;
using TMPro;

public class ItemMatchEvent : MonoBehaviour
{
    public TextMeshProUGUI textGameTime;
    public TextMeshProUGUI textScore;
    public TextMeshProUGUI textEventType;
    public TextMeshProUGUI textMain;
    public TextMeshProUGUI textPoint;
    public TextMeshProUGUI textAssist;

    private int matchEventIndex;

    public void StartThis(int _matchIndex, int _matchEventIndex, int _scoreA, int _scoreB)
    {
        MatchEvent me = SaveData.SD.matches[_matchIndex].events[_matchEventIndex];
        matchEventIndex = _matchEventIndex;
        textGameTime.text = Helpers.TimeSpanToString(me.gameTime, true);
        textScore.text = _scoreA + "-" + _scoreB;
        textEventType.text = me.EventTypeString();

        switch (me.eventType)
        {
            case MatchEventType.POINT:
                textPoint.text = me.playerMain.Identification();
                textAssist.text = me.eventType == MatchEventType.POINT ? "(" + me.playerAssist.Identification() + ")" : string.Empty;
                break;
            case MatchEventType.DEFENSE:
            case MatchEventType.CALLAHAN:
                textMain.text = me.playerMain.Identification();
                break;
            case MatchEventType.TIMEOUT:
            case MatchEventType.SPIRIT_TIMEOUT:
                textMain.text = me.teamA ? SaveData.SD.matches[_matchIndex].teamA.myName : SaveData.SD.matches[_matchIndex].teamB.myName;
                break;
        }
    }

    public void Clicked()
    {
        MenuMatches.MM.OpenMatchEvent(matchEventIndex);
    }
}
