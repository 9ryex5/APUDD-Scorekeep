using UnityEngine;
using TMPro;

public class ItemMatchEvent : MonoBehaviour
{
    public TextMeshProUGUI textGameTime;
    public TextMeshProUGUI textEventType;
    public TextMeshProUGUI textPlayerMain;
    public TextMeshProUGUI textPlayerAsist;

    private MatchEvent matchEvent;

    public void StartThis(MatchEvent _me)
    {
        matchEvent = _me;
        textGameTime.text = Helpers.TimeSpanToString(_me.gameTime);
        textEventType.text = matchEvent.EventTypeString();
        if (matchEvent.eventType == MatchEventType.TIMEOUT || matchEvent.eventType == MatchEventType.SPIRIT_TIMEOUT)
        {
            //TODO team names
        }
        else
        {
            textPlayerMain.text = matchEvent.playerMain.Identification();
            textPlayerAsist.text = matchEvent.eventType == MatchEventType.POINT ? "(" + matchEvent.playerAssist.Identification() + ")" : string.Empty;
        }
    }
}
