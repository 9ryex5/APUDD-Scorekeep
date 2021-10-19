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
        textGameTime.text = matchEvent.gameTime.Hours.ToString("00") + ":" + matchEvent.gameTime.Minutes.ToString("00") + ":" + matchEvent.gameTime.Seconds.ToString("00");
        textEventType.text = matchEvent.eventType.ToString();
        textPlayerMain.text = matchEvent.playerMain.myName;
        textPlayerAsist.text = matchEvent.playerAssist.myName;
    }
}
