using UnityEngine;
using TMPro;

public class ItemPlayer : MonoBehaviour
{
    public TextMeshProUGUI myText;

    private Player player;

    public void StartThis(Player _player)
    {
        player = _player;
        myText.text = _player.number.ToString();
    }

    public void Clicked()
    {

    }

    public Player GetPlayer()
    {
        return player;
    }
}
