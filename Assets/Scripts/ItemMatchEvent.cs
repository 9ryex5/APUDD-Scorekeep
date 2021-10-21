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
        textGameTime.text = (matchEvent.gameTime.Hours > 0 ? matchEvent.gameTime.Hours.ToString("00") + ":" : string.Empty) + matchEvent.gameTime.Minutes.ToString("00") + ":" + matchEvent.gameTime.Seconds.ToString("00");
        textEventType.text = matchEvent.EventTypeString();
        textPlayerMain.text = matchEvent.playerMain.Identification();
        textPlayerAsist.text = matchEvent.eventType == MatchEventType.POINT ? "(" + matchEvent.playerAssist.Identification() + ")" : string.Empty;
    }
}
