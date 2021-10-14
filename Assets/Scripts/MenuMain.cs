using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuMain : MonoBehaviour
{
    public TextMeshProUGUI textMatch;
    public Image imageFlag;
    public Sprite[] spritesFlag;

    private void Start()
    {
        Language();
    }

    public void ButtonFlag()
    {
        ManagerLanguages.ML.NextLanguage();
        Language();
    }

    private void Language()
    {
        textMatch.text = ManagerLanguages.ML.Translate("Match");
        imageFlag.sprite = spritesFlag[(int)ManagerSaveData.MSD.settings.language];
    }
}
