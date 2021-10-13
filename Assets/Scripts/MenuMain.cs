using UnityEngine;
using UnityEngine.UI;

public class MenuMain : MonoBehaviour
{
    public Image imageFlag;
    public Sprite[] spritesFlag;

    private void Start()
    {
        UpdateFlag();
    }

    public void ButtonFlag()
    {
        ManagerLanguages.ML.NextLanguage();
        UpdateFlag();
    }

    private void UpdateFlag()
    {
        imageFlag.sprite = spritesFlag[(int)ManagerSaveData.MSD.settings.language];
    }
}
