using UnityEngine;
using TMPro;

public class ItemPlayer : MonoBehaviour
{
    public TextMeshProUGUI myText;

    private Player player;
    private bool teamA;
    private bool playing;

    public void StartThis(Player p, bool _teamA, bool _playing, bool _showName = false)
    {
        player = p;
        SetText(_showName);
        teamA = _teamA;
        playing = _playing;
    }

    public void Clicked()
    {
        if (playing)
            ManagerPlaying.MP.ClickedPlayer(this);
        else
            MenuMatches.MM.ClickedPlayer(this);
    }

    public void SetText(bool _name)
    {
        if (_name)
        {
            myText.text = player.GetName(true, 0) + "\n" + player.GetName(true, 1);
            myText.fontSize = Screen.height * 0.0225f;
        }
        else
        {
            myText.text = player.number.ToString();
            myText.fontSize = Screen.height * 0.06875f;
        }
    }

    public bool GetTeamA()
    {
        return teamA;
    }

    public Player GetPlayer()
    {
        return player;
    }
}
