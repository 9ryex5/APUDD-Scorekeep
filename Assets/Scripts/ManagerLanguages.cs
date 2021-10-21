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
        {"SettingMatch", "Setting Match"},
        {"ImportMatch", "You need to import a match first"},
        {"Start", "Start" },
        {"MatchInfo", "Match Info"},
        {"Halftime", "Halftime"},
        {"Fulltime", "Fulltime"},
        {"Undo", "Undo" },
        {"Timeout", "Timeout"},
        {"CalledBy", "Called By"},
        {"SpiritTimeout", "Spirit Timeout" },
        {"End", "End" },
        {"NoEventsUndo", "There are no events to undo"},
        {"TimeNotRunning", "Time is not running"},
        {"NoTie", "The match cannot end in a tie"},
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
        {"SettingMatch", "Preparar Partida"},
        {"ImportMatch", "Tem que importar uma partida primeiro"},
        {"Start", "Começar" },
        {"MatchInfo", "Info Partida"},
        {"Halftime", "Halftime"},
        {"Fulltime", "Fulltime"},
        {"Undo", "Anular" },
        {"Timeout", "Timeout"},
        {"CalledBy", "Chamado Por"},
        {"SpiritTimeout", "Timeout de Espírito" },
        {"End", "Terminar" },
        {"NoEventsUndo", "Não há eventos para anular"},
        {"TimeNotRunning", "O tempo não está a correr"},
        {"NoTie", "A partida não pode terminar num empate"},
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
