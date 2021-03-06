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
        {"TimeOver", "Time is over"},
        {"FirstPull", "First Pull"},
        {"Undo", "Undo" },
        {"Timeout", "Timeout"},
        {"CalledBy", "Called By"},
        {"SpiritTimeout", "Spirit Timeout" },
        {"End", "End" },
        {"Exit", "Exit"},
        {"SaveMatch", "Save Match"},
        {"DiscardMatch", "Discard Match"},
        {"NoEventsUndo", "There are no events to undo"},
        {"TimeNotRunning", "Time is not running"},
        {"NoTie", "The match cannot end in a tie"},
        {"Point", "Point" },
        {"Assistance", "Assistance" },
        {"Defense", "Defense" },
        {"Callahan", "Callahan"},
        {"WhoPoint", "Who Scored?" },
        {"WhoAssist", "Who Assisted?" },
        {"Confirm", "Confirm"},
        {"Cancel", "Cancel"},
        {"DeleteEvent", "Delete Event?"},
        {"ChangePlayer", "Change Player"}
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
        {"TimeOver", "Tempo acabou"},
        {"FirstPull", "Primeiro Pull"},
        {"Undo", "Anular" },
        {"Timeout", "Timeout"},
        {"CalledBy", "Chamado Por"},
        {"SpiritTimeout", "Timeout de Espírito" },
        {"End", "Terminar" },
        {"Exit", "Sair"},
        {"SaveMatch", "Guardar Partida"},
        {"DiscardMatch", "Descartar Partida"},
        {"NoEventsUndo", "Não há eventos para anular"},
        {"TimeNotRunning", "O tempo não está a correr"},
        {"NoTie", "A partida não pode terminar num empate"},
        {"Point", "Ponto" },
        {"Assistance", "Assistência" },
        {"Defense", "Defesa" },
        {"Callahan", "Callahan"},
        {"WhoPoint", "Quem Pontuou?" },
        {"WhoAssist", "Quem Assistiu?" },
        {"Confirm", "Confirmar"},
        {"Cancel", "Cancelar"},
        {"DeleteEvent", "Apagar Evento?"},
        {"ChangePlayer", "Alterar Jogador"}
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
