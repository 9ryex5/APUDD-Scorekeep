using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManagerPlaying : MonoBehaviour
{
    public static ManagerPlaying MP;

    public TextMeshProUGUI textTimer;
    public TextMeshProUGUI textScore;

    private DateTime startTime;
    private TimeSpan stoppedTime;
    private DateTime startStoppage;

    public TextMeshProUGUI textTeamA, textTeamB;
    public Transform parentA, parentB;
    public ItemPlayer prefabItemPlayer;
    private List<ItemPlayer> itemsPlayer;

    //Settings
    public Button buttonStartTime;
    public Button buttonPlayersInfo;
    public Button buttonOptions;
    public Sprite[] spritesButtonPlayerInfo;
    private bool showingNames;

    //Options
    public Button buttonUndo;
    public TextMeshProUGUI textButtonUndo;
    public Button buttonTimeout;
    public TextMeshProUGUI textButtonTimeout;
    public Button buttonSpiritTimeout;
    public TextMeshProUGUI textButtonSpiritTimeout;
    public Button buttonEnd;
    public TextMeshProUGUI textButtonEnd;

    //Info
    public TextMeshProUGUI textLabelHalfTime, textLabelFullTime;
    public TextMeshProUGUI textHalfTime, textFullTime;

    //Undo
    public TextMeshProUGUI textUndoTitle;
    public TextMeshProUGUI textEventPointType;
    public TextMeshProUGUI textEventPointPlayer;
    public TextMeshProUGUI textEventAssistType;
    public TextMeshProUGUI textEventAssistPlayer;
    public TextMeshProUGUI textEventType;
    public TextMeshProUGUI textEventPlayer;

    //Timeout
    public TextMeshProUGUI textCalledBy;
    public TextMeshProUGUI textTimeoutTeamA;
    public TextMeshProUGUI textTimeoutTeamB;

    //Event
    public TextMeshProUGUI textEventTitle;
    public TextMeshProUGUI textButtonPoint;
    public TextMeshProUGUI textButtonAssist;
    public TextMeshProUGUI textButtonDefense;
    public TextMeshProUGUI textButtonCallahan;
    private ItemPlayer currentItemPlayer;
    public TextMeshProUGUI textPlayerEventPoint;
    public Transform parentPointPlayers;

    private Match match;
    private bool matchStarted;
    private bool timeIsRunning;
    private TimeSpan gameTime;
    private bool isPoint;
    private bool isAssist;
    private int debugCount;
    private void Awake()
    {
        MP = this;
    }

    private void Update()
    {
        if (!timeIsRunning) return;

        gameTime = (DateTime.Now - startTime) - stoppedTime;
        textTimer.text = gameTime.Hours.ToString("00") + ":" + gameTime.Minutes.ToString("00") + ":" + gameTime.Seconds.ToString("00");
    }

    public void StartMatch(Match _m)
    {
        match = _m;
        matchStarted = false;
        timeIsRunning = false;
        textTimer.text = "00:00:00";
        UpdateScore();
        UpdateInfo();
        SetTeams();
        buttonStartTime.gameObject.SetActive(true);
        buttonOptions.interactable = false;
        buttonUndo.interactable = false;
        buttonEnd.interactable = false;
        Language();
    }

    private void SetTeams()
    {
        for (int i = 0; i < parentA.childCount; i++)
            Destroy(parentA.GetChild(i).gameObject);

        for (int i = 0; i < parentB.childCount; i++)
            Destroy(parentB.GetChild(i).gameObject);

        textTeamA.text = match.teamA.myName;
        textTeamB.text = match.teamB.myName;
        textTimeoutTeamA.text = match.teamA.myName;
        textTimeoutTeamB.text = match.teamB.myName;

        itemsPlayer = new List<ItemPlayer>();

        for (int i = 0; i < match.teamA.players.Count; i++)
        {
            ItemPlayer ip = Instantiate(prefabItemPlayer, parentA);
            ip.StartThis(match.teamA.players[i], true);
            itemsPlayer.Add(ip);
        }

        for (int i = 0; i < match.teamB.players.Count; i++)
        {
            ItemPlayer ip = Instantiate(prefabItemPlayer, parentB);
            ip.StartThis(match.teamB.players[i], false);
            itemsPlayer.Add(ip);
        }
    }

    public void ButtonInfo()
    {
        ManagerUI.MUI.OpenLayout(ManagerUI.MUI.playingInfo);
    }

    public void ButtonStartTime()
    {
        if (!matchStarted)
        {
            startTime = DateTime.Now;
            matchStarted = true;
        }
        else
        {
            stoppedTime += DateTime.Now - startStoppage;
            buttonTimeout.interactable = true;
            buttonSpiritTimeout.interactable = true;
        }

        timeIsRunning = true;
        buttonStartTime.gameObject.SetActive(false);
        buttonOptions.interactable = true;
    }

    public void ButtonPlayerInfo()
    {
        showingNames = !showingNames;

        buttonPlayersInfo.GetComponent<Image>().sprite = spritesButtonPlayerInfo[showingNames ? 1 : 0];

        foreach (ItemPlayer ip in itemsPlayer)
            ip.myText.text = showingNames ? ip.GetPlayer().myName : ip.GetPlayer().number.ToString();
    }

    public void ButtonUndo()
    {
        textEventPointType.text = string.Empty;
        textEventPointPlayer.text = string.Empty;
        textEventAssistType.text = string.Empty;
        textEventAssistPlayer.text = string.Empty;
        textEventType.text = string.Empty;
        textEventPlayer.text = string.Empty;

        textUndoTitle.text = ManagerLanguages.ML.Translate("Undo");

        switch (match.events.Last().eventType)
        {
            case MatchEventType.POINT:
                textEventPointType.text = ManagerLanguages.ML.Translate("Point");
                textEventPointPlayer.text = PlayerIdentification(match.events.Last().playerMain);
                textEventAssistType.text = ManagerLanguages.ML.Translate("Assistance");
                textEventAssistPlayer.text = PlayerIdentification(match.events.Last().playerAssist);
                break;
            case MatchEventType.DEFENSE:
                textEventType.text = ManagerLanguages.ML.Translate("Defense");
                textEventPlayer.text = PlayerIdentification(match.events.Last().playerMain);
                break;
            case MatchEventType.CALLAHAN:
                textEventType.text = "Callahan";
                textEventPlayer.text = PlayerIdentification(match.events.Last().playerMain);
                break;
            case MatchEventType.TIMEOUT:
                textEventType.text = "Timeout";
                textEventPlayer.text = match.events.Last().teamA ? match.teamA.myName : match.teamB.myName;
                break;
        }

        ManagerUI.MUI.OpenLayout(ManagerUI.MUI.playingUndo);
    }

    public void ButtonConfirmUndo()
    {
        match.events.RemoveAt(match.events.Count - 1);
        UpdateScore();
        if (match.events.Count == 0) buttonUndo.interactable = false;
        ManagerUI.MUI.OpenLayout(ManagerUI.MUI.playing);
    }

    public void ButtonTimeout()
    {
        ManagerUI.MUI.OpenLayout(ManagerUI.MUI.playingTimeout);
    }

    public void ButtonConfirmTimeout(bool _teamA)
    {
        AddMatchTimeout(_teamA);
        ManagerUI.MUI.OpenLayout(ManagerUI.MUI.playing);
    }

    public void ButtonSpiritTimeout()
    {
        startStoppage = DateTime.Now;
        timeIsRunning = false;
        buttonStartTime.gameObject.SetActive(true);
        buttonTimeout.interactable = false;
        buttonSpiritTimeout.interactable = false;

        ManagerUI.MUI.OpenLayout(ManagerUI.MUI.playing);
    }

    public void ButtonEnd()
    {
        SaveData.SD.SaveMatch(match);
        ManagerUI.MUI.OpenLayout(ManagerUI.MUI.menuPlay);
    }

    public void ClickedPlayer(ItemPlayer ip)
    {
        if (!timeIsRunning) return;

        if (isPoint || isAssist)
        {
            if (isAssist)
            {
                ItemPlayer assist = currentItemPlayer;
                currentItemPlayer = ip;
                AddMatchEvent(MatchEventType.POINT, assist.GetPlayer());
            }
            else
            {
                AddMatchEvent(MatchEventType.POINT, ip.GetPlayer());
            }

            ManagerUI.MUI.OpenLayout(ManagerUI.MUI.playing);
            isPoint = false;
            isAssist = false;
            return;
        }

        currentItemPlayer = ip;
        textEventTitle.text = PlayerIdentification(ip.GetPlayer());
        ManagerUI.MUI.OpenLayout(ManagerUI.MUI.playingEvent);
    }

    public void ButtonPoint()
    {
        SetPlayersEventPoint();
        textPlayerEventPoint.text = ManagerLanguages.ML.Translate("WhoAssist");
        ManagerUI.MUI.OpenLayout(ManagerUI.MUI.playingEventPoint);
        isPoint = true;
    }

    public void ButtonAssist()
    {
        SetPlayersEventPoint();
        textPlayerEventPoint.text = ManagerLanguages.ML.Translate("WhoPoint");
        ManagerUI.MUI.OpenLayout(ManagerUI.MUI.playingEventPoint);
        isAssist = true;
    }

    private void SetPlayersEventPoint()
    {
        for (int i = 0; i < parentPointPlayers.childCount; i++)
            Destroy(parentPointPlayers.GetChild(i).gameObject);

        if (currentItemPlayer.GetTeamA())
        {
            for (int i = 0; i < match.teamA.players.Count; i++)
            {
                if (currentItemPlayer.GetPlayer().ID == match.teamA.players[i].ID) continue;
                ItemPlayer ip = Instantiate(prefabItemPlayer, parentPointPlayers);
                ip.StartThis(match.teamA.players[i], true);
            }
        }
        else
        {
            for (int i = 0; i < match.teamB.players.Count; i++)
            {
                if (currentItemPlayer.GetPlayer().ID == match.teamB.players[i].ID) continue;
                ItemPlayer ip = Instantiate(prefabItemPlayer, parentPointPlayers);
                ip.StartThis(match.teamB.players[i], false);
            }
        }
    }

    public void ButtonDefense()
    {
        AddMatchEvent(MatchEventType.DEFENSE);
        ManagerUI.MUI.OpenLayout(ManagerUI.MUI.playing);
    }

    public void ButtonCallahan()
    {
        AddMatchEvent(MatchEventType.CALLAHAN);
        ManagerUI.MUI.OpenLayout(ManagerUI.MUI.playing);
    }

    private void AddMatchEvent(MatchEventType met, Player assist = new Player())
    {
        match.events.Add(new MatchEvent()
        {
            gameTime = gameTime,
            eventType = met,
            teamA = currentItemPlayer.GetTeamA(),
            playerMain = currentItemPlayer.GetPlayer(),
            playerAssist = assist
        });

        if (met == MatchEventType.POINT || met == MatchEventType.CALLAHAN) UpdateScore();
        buttonUndo.interactable = true;
    }

    private void AddMatchTimeout(bool _teamA)
    {
        match.events.Add(new MatchEvent()
        {
            gameTime = gameTime,
            eventType = MatchEventType.TIMEOUT,
            teamA = _teamA,
            playerMain = new Player(),
            playerAssist = new Player()
        });

        buttonUndo.interactable = true;
    }

    private void UpdateScore()
    {
        int a = match.GetScore(true);
        int b = match.GetScore(false);
        textScore.text = a + "-" + b;
        buttonEnd.interactable = a != b;
    }

    private void UpdateInfo()
    {
        textHalfTime.text = match.halfTime.Hours.ToString("00") + ":" + match.halfTime.Minutes.ToString("00") + ":" + match.halfTime.Seconds.ToString("00");
        textFullTime.text = match.fullTime.Hours.ToString("00") + ":" + match.fullTime.Minutes.ToString("00") + ":" + match.fullTime.Seconds.ToString("00");
    }

    private string PlayerIdentification(Player p)
    {
        return p.number + " - " + p.myName;
    }

    private void Language()
    {
        textLabelHalfTime.text = ManagerLanguages.ML.Translate("Halftime");
        textLabelFullTime.text = ManagerLanguages.ML.Translate("Fulltime");
        textButtonUndo.text = ManagerLanguages.ML.Translate("Undo");
        textButtonTimeout.text = ManagerLanguages.ML.Translate("Timeout");
        textCalledBy.text = ManagerLanguages.ML.Translate("CalledBy");
        textButtonSpiritTimeout.text = ManagerLanguages.ML.Translate("SpiritTimeout");
        textButtonEnd.text = ManagerLanguages.ML.Translate("End");
        textButtonPoint.text = ManagerLanguages.ML.Translate("Point");
        textButtonAssist.text = ManagerLanguages.ML.Translate("Assistance");
        textButtonDefense.text = ManagerLanguages.ML.Translate("Defense");
        textButtonCallahan.text = ManagerLanguages.ML.Translate("Callahan");
    }
}
