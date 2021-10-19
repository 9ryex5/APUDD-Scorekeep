using UnityEngine;
using TMPro;

public class ItemMatch : MonoBehaviour
{
    public TextMeshProUGUI textDate;
    public TextMeshProUGUI textScore;
    public TextMeshProUGUI textTeams;

    private Match match;

    public void StartThis(Match _m)
    {
        match = _m;
        textDate.text = match.date.Year + "/" + match.date.Month + "/" + match.date.Day;
        textScore.text = match.GetScore(true) + " - " + match.GetScore(false);
        textTeams.text = match.teamA.myName + " | " + match.teamB.myName;
    }

    public void Clicked()
    {
        MenuMatches.MM.OpenMatch(match);
    }
}
