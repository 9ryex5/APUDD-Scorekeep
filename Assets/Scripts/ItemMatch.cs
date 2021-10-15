using UnityEngine;
using TMPro;

public class ItemMatch : MonoBehaviour
{
    public TextMeshProUGUI myText;

    private Match match;

    public void StartThis(Match _m)
    {
        match = _m;
        myText.text = match.teamA.myName + " | " + match.teamB.myName;
    }

    public void Clicked()
    {
        MenuMatches.MM.OpenMatch(match);
    }
}
