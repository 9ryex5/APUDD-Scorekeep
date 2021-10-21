using UnityEngine;
using TMPro;

public class ItemPlayer : MonoBehaviour
{
    public TextMeshProUGUI myText;

    private Player player;
    private bool teamA;

    public void StartThis(Player p, bool _teamA)
    {
        player = p;
        myText.text = p.number.ToString();
        FontSizeNumber();
        teamA = _teamA;
    }

    public void Clicked()
    {
        ManagerPlaying.MP.ClickedPlayer(this);
    }

    public bool GetTeamA()
    {
        return teamA;
    }

    public Player GetPlayer()
    {
        return player;
    }

    public void FontSizeName()
    {
        myText.fontSize = 18;
    }

    public void FontSizeNumber()
    {
        myText.fontSize = 55;
    }
}
