using System.Collections.Generic;
using UnityEngine;

public class ManagerLanguages : MonoBehaviour
{
    public static ManagerLanguages ML;

    private Dictionary<string, string> currentDictionary;

    private Dictionary<string, string> EN = new Dictionary<string, string>
    {
        {"Play", "Play" },
        {"Matches", "Matches"},
        {"Import", "Import"},
        {"Start", "Start" },
        {"Undo", "Undo" },
        {"Timeout", "Timeout"},
        {"CalledBy", "Called By"},
        {"SpiritTimeout", "Spirit Timeout" },
        {"End", "End" },
        {"Point", "Point" },
        {"Assistance", "Assistance" },
        {"Defense", "Defense" },
        {"Callahan", "Callahan"},
        {"WhoPoint", "Who Scored?" },
        {"WhoAssist", "Who Assisted?" }
    };

    private Dictionary<string, string> PT = new Dictionary<string, string>
    {
        {"Play", "Jogar" },
        {"Matches", "Partidas"},
        {"Import", "Importar"},
        {"Start", "Começar" },
        {"Undo", "Anular" },
        {"Timeout", "Timeout"},
        {"CalledBy", "Chamado Por"},
        {"SpiritTimeout", "Timeout de Espírito" },
        {"End", "Terminar" },
        {"Point", "Ponto" },
        {"Assistance", "Assistência" },
        {"Defense", "Defesa" },
        {"Callahan", "Callahan"},
        {"WhoPoint", "Quem Pontuou?" },
        {"WhoAssist", "Quem Assistiu?" }
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
        if (temp == null)
        {
            Debug.LogWarning("Translation not found");
            return _key;
        }
        return temp;
    }

    public void NextLanguage()
    {
        SaveData.SD.settings.language++;
        if ((int)SaveData.SD.settings.language >= System.Enum.GetValues(typeof(Language)).Length) SaveData.SD.settings.language = 0;
        UpdateDictionary();
        SaveData.SD.SaveSettings();
    }

    private void UpdateDictionary()
    {
        switch (SaveData.SD.settings.language)
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
