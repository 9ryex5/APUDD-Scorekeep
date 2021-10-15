using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuMain : MonoBehaviour
{
    public TextMeshProUGUI textPlay;
    public TextMeshProUGUI textMatches;
    public Image imageFlag;
    public Sprite[] spritesFlag;

    private void Start()
    {
        Language();
    }

    public void ButtonPlay()
    {
        ManagerUI.MUI.OpenLayout(ManagerUI.MUI.menuPlay);
    }

    public void ButtonMatches()
    {
        ManagerUI.MUI.OpenLayout(ManagerUI.MUI.menuMatches);
    }

    public void ButtonFlag()
    {
        ManagerLanguages.ML.NextLanguage();
        Language();
    }

    private void Language()
    {
        textPlay.text = ManagerLanguages.ML.Translate("Play");
        textMatches.text = ManagerLanguages.ML.Translate("Matches");
        imageFlag.sprite = spritesFlag[(int)SaveData.SD.settings.language];
    }
}
