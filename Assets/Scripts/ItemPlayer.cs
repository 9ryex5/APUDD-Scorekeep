using UnityEngine;
using TMPro;

public class ItemPlayer : MonoBehaviour
{
    public TextMeshProUGUI myText;

    private Player player;
    private bool teamA;

    public void StartThis(Player p, bool _teamA, bool _showName = false)
    {
        player = p;
        SetText(_showName);
        teamA = _teamA;
    }

    public void Clicked()
    {
        ManagerPlaying.MP.ClickedPlayer(this);
    }

    public void SetText(bool _name)
    {
        if (_name)
        {
            myText.text = player.GetName(true, 0) + "\n" + player.GetName(true, 1);
            myText.fontSize = 18;
        }
        else
        {
            myText.text = player.number.ToString();
            myText.fontSize = 55;
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
