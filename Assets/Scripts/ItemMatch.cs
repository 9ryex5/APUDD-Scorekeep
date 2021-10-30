using UnityEngine;
using TMPro;

public class ItemMatch : MonoBehaviour
{
    public TextMeshProUGUI textDate;
    public TextMeshProUGUI textTime;
    public TextMeshProUGUI textScore;
    public TextMeshProUGUI textTeamA, textTeamB;

    private int matchIndex;

    public void StartThis(int _matchIndex)
    {
        Match m = SaveData.SD.matches[_matchIndex];
        matchIndex = _matchIndex;
        textDate.text = Helpers.DateTimeToString(m.date, true);
        textTime.text = Helpers.DateTimeToString(m.date, false, false);
        textTeamA.text = m.teamA.myName;
        textTeamB.text = m.teamB.myName;
        textScore.text = m.GetScore(true) + " - " + m.GetScore(false);
    }

    public void Clicked()
    {
        MenuMatches.MM.OpenMatch(matchIndex);
    }
}
