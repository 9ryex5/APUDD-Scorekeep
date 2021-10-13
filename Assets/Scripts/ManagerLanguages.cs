using System.Collections.Generic;
using UnityEngine;

public class ManagerLanguages : MonoBehaviour
{
    public static ManagerLanguages ML;

    private Dictionary<string, string> currentDictionary;

    private Dictionary<string, string> EN = new Dictionary<string, string>
    {
        {"Play", "Play" },
        {"Quit", "Quit" }
    };

    private Dictionary<string, string> PT = new Dictionary<string, string>
    {
        {"Play", "Jogar" },
        {"Quit", "Desistir" }
    };

    private void Awake()
    {
        ML = this;
    }

    private void Start()
    {
        UpdateDictionary();
    }

    public string Translate(string _key)
    {
        currentDictionary.TryGetValue(_key, out string temp);
        if (temp == null) return _key;
        return temp;
    }

    public void NextLanguage()
    {
        ManagerSaveData.MSD.settings.language++;
        if ((int)ManagerSaveData.MSD.settings.language >= System.Enum.GetValues(typeof(Language)).Length) ManagerSaveData.MSD.settings.language = 0;
        UpdateDictionary();
        ManagerSaveData.MSD.SaveSettings();
    }

    private void UpdateDictionary()
    {
        switch (ManagerSaveData.MSD.settings.language)
        {
            case Language.EN:
                currentDictionary = EN;
                break;
            case Language.PT:
                currentDictionary = PT;
                break;
        }
    }
}
