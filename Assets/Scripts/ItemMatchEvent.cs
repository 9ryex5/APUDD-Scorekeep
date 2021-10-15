using UnityEngine;
using TMPro;

public class ItemMatchEvent : MonoBehaviour
{
    public TextMeshProUGUI myText;

    private MatchEvent matchEvent;

    public void StartThis(MatchEvent _me)
    {
        matchEvent = _me;
        myText.text = matchEvent.eventType.ToString();
    }
}
