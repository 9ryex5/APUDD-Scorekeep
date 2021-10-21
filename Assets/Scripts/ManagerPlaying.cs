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
    public TextMeshProUGUI textTimerExtra;

    private const int timeToPullYellow = 75;
    private const int timeToPullRed = 90;

    private Match match;
    private bool matchStarted;
    private bool timeIsRunning;
    private DateTime startTime;
    private TimeSpan gameTime;
    private TimeSpan stoppedTime;
    private DateTime startStoppage;
    private TimeSpan eventExactTime, possibleEventExactTime;
    private bool timerExtra;
    private bool warnedHalfTime, warnedFullTime;
    private bool isPoint;
    private bool isAssist;

    //Teams
    public TextMeshProUGUI textTeamA, textTeamB;
    public Transform parentA, parentB;
    public ItemPlayer prefabItemPlayer;
    private List<ItemPlayer> itemsPlayer, itemsPlayerPoint;
    public Image genderPullA, genderPullB;
    public Image genderPullAPrevious, genderPullBPrevious;
    public Image genderPullANext, genderPullBNext;
    private bool firstPullFemale;

    //Settings
    public GameObject imageWhistle;
    public Button buttonStartTime;
    public Image buttonPlayersInfo;
    public Sprite[] spritesButtonPlayerInfo;
    private bool showingNames, showingNamesPoint;

    //Options
    public TextMeshProUGUI textButtonUndo;
    public TextMeshProUGUI textButtonTimeout;
    public TextMeshProUGUI textButtonSpiritTimeout;
    public TextMeshProUGUI textButtonEnd;

    //Info
    public TextMeshProUGUI textInfoTitle;
    public TextMeshProUGUI textLabelHalfTime, textLabelFullTime;
    public TextMeshProUGUI textHalfTime, textFullTime;
    public TextMeshProUGUI textLabelFirstPullGender;
    public Image imageFirstPullMale, imageFirstPullFemale;

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
    public TextMeshProUGUI textCalledBySpirit;
    public TextMeshProUGUI textTimeoutTeamASpirit;
    public TextMeshProUGUI textTimeoutTeamBSpirit;

    //Event
    public TextMeshProUGUI textEventTitle;
    public TextMeshProUGUI textButtonPoint;
    public TextMeshProUGUI textButtonAssist;
    public TextMeshProUGUI textButtonDefense;
    public TextMeshProUGUI textButtonCallahan;
    private ItemPlayer currentItemPlayer;
    public TextMeshProUGUI textPlayerEventPoint;
    public Image buttonPlayersInfoPoint;
    public Transform parentPointPlayers;

    //Exit
    public TextMeshProUGUI textExitTitle;
    public TextMeshProUGUI textSaveMatch;
    public TextMeshProUGUI textDiscardMatch;

    private void Awake()
    {
        MP = this;
    }

    private void Update()
    {
        if (!timeIsRunning) return;

        if (timerExtra)
        {
            float s = Mathf.FloorToInt((float)(gameTime - eventExactTime).TotalSeconds);
            textTimerExtra.text = s.ToString();
            textTimerExtra.color = TimeToPullColor(s);
        }

        UpdateGameTime();

        if (!warnedHalfTime && gameTime >= match.halfTime)
        {
            ManagerUI.MUI.Warning(ManagerLanguages.ML.Translate("Halftime"));
            warnedHalfTime = true;
        }

        if (!warnedFullTime && gameTime >= match.fullTime)
        {
            ManagerUI.MUI.Warning(ManagerLanguages.ML.Translate("TimeOver"));
            warnedFullTime = true;
        }
    }

    public void StartMatch(Match _m)
    {
        match = _m;
        matchStarted = false;
        timeIsRunning = false;
        textTimer.text = "00:00";
        warnedHalfTime = false;
        warnedFullTime = false;
        UpdateScore();
        ButtonFirstGenderPull(firstPullFemale);
        ButtonTimerExtra();
        UpdateInfo();
        SetTeams();
        buttonStartTime.gameObject.SetActive(true);
        buttonPlayersInfo.sprite = spritesButtonPlayerInfo[0];
        buttonPlayersInfoPoint.sprite = spritesButtonPlayerInfo[0];
        showingNames = false;
        showingNamesPoint = false;
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
        textTimeoutTeamASpirit.text = match.teamA.myName;
        textTimeoutTeamBSpirit.text = match.teamB.myName;
        genderPullA.gameObject.SetActive(false);
        genderPullAPrevious.gameObject.SetActive(false);
        genderPullANext.gameObject.SetActive(false);
        genderPullB.gameObject.SetActive(false);
        genderPullBPrevious.gameObject.SetActive(false);
        genderPullBNext.gameObject.SetActive(false);

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

    public void ButtonFirstGenderPull(bool _female)
    {
        firstPullFemale = _female;
        imageFirstPullFemale.color = new Color(imageFirstPullFemale.color.r, imageFirstPullFemale.color.g, imageFirstPullFemale.color.b, _female ? 1 : 0.5f);
        imageFirstPullMale.color = new Color(imageFirstPullMale.color.r, imageFirstPullMale.color.g, imageFirstPullMale.color.b, _female ? 0.5f : 1);
    }

    public void ButtonTimerExtra()
    {
        textTimerExtra.text = string.Empty;
        imageWhistle.SetActive(false);
        timerExtra = false;

        genderPullA.gameObject.SetActive(false);
        genderPullAPrevious.gameObject.SetActive(false);
        genderPullANext.gameObject.SetActive(false);
        genderPullB.gameObject.SetActive(false);
        genderPullBPrevious.gameObject.SetActive(false);
        genderPullBNext.gameObject.SetActive(false);
    }

    public void ButtonStartTime()
    {
        if (!matchStarted)
        {
            startTime = DateTime.Now;
            matchStarted = true;
        }
        else
            stoppedTime += DateTime.Now - startStoppage;

        timeIsRunning = true;
        buttonStartTime.gameObject.SetActive(false);
    }

    public void ButtonPlayerInfo()
    {
        showingNames = !showingNames;

        buttonPlayersInfo.sprite = spritesButtonPlayerInfo[showingNames ? 1 : 0];

        foreach (ItemPlayer ip in itemsPlayer)
            ip.SetText(showingNames);
    }

    public void ButtonOptions()
    {
        possibleEventExactTime = gameTime;
        ManagerUI.MUI.OpenLayout(ManagerUI.MUI.playingOptions);
    }

    public void ButtonUndo()
    {
        if (match.events.Count == 0)
        {
            ManagerUI.MUI.Warning(ManagerLanguages.ML.Translate("NoEventsUndo"));
            return;
        }

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
                textEventPointPlayer.text = match.events.Last().playerMain.Identification();
                textEventAssistType.text = ManagerLanguages.ML.Translate("Assistance");
                textEventAssistPlayer.text = match.events.Last().playerAssist.Identification();
                break;
            case MatchEventType.DEFENSE:
                textEventType.text = ManagerLanguages.ML.Translate("Defense");
                textEventPlayer.text = match.events.Last().playerMain.Identification();
                break;
            case MatchEventType.CALLAHAN:
                textEventType.text = "Callahan";
                textEventPlayer.text = match.events.Last().playerMain.Identification();
                break;
            case MatchEventType.TIMEOUT:
            case MatchEventType.SPIRIT_TIMEOUT:
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
        ManagerUI.MUI.OpenLayout(ManagerUI.MUI.playing);
    }

    public void ButtonTimeout()
    {
        if (timeIsRunning)
            ManagerUI.MUI.OpenLayout(ManagerUI.MUI.playingTimeout);
        else
            ManagerUI.MUI.Warning(ManagerLanguages.ML.Translate("TimeNotRunning"));
    }

    public void ButtonConfirmTimeout(bool _teamA)
    {
        AddMatchTimeout(_teamA, false);
        eventExactTime = possibleEventExactTime;
        ManagerUI.MUI.OpenLayout(ManagerUI.MUI.playing);
    }

    public void ButtonSpiritTimeout()
    {
        if (timeIsRunning)
            ManagerUI.MUI.OpenLayout(ManagerUI.MUI.playingSpiritTimeout);
        else
            ManagerUI.MUI.Warning(ManagerLanguages.ML.Translate("TimeNotRunning"));
    }

    public void ButtonConfirmSpiritTimeout(bool _teamA)
    {
        AddMatchTimeout(_teamA, true);
        startStoppage = DateTime.Now;
        timeIsRunning = false;
        UpdateGameTime();
        buttonStartTime.gameObject.SetActive(true);

        ManagerUI.MUI.OpenLayout(ManagerUI.MUI.playing);
    }

    public void ButtonEnd()
    {
        if (match.GetScore(true) == match.GetScore(false))
        {
            ManagerUI.MUI.Warning(ManagerLanguages.ML.Translate("NoTie"));
            return;
        }

        SaveData.SD.SaveMatch(match);
        ManagerUI.MUI.OpenLayout(ManagerUI.MUI.menuPlay);
    }

    public void ClickedPlayer(ItemPlayer ip)
    {
        if (!timeIsRunning)
        {
            ManagerUI.MUI.Warning(ManagerLanguages.ML.Translate("TimeNotRunning"));
            return;
        }

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
        eventExactTime = gameTime;
        textEventTitle.text = ip.GetPlayer().Identification();
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

        itemsPlayerPoint = new List<ItemPlayer>();

        if (currentItemPlayer.GetTeamA())
        {
            for (int i = 0; i < match.teamA.players.Count; i++)
            {
                if (currentItemPlayer.GetPlayer().ID == match.teamA.players[i].ID) continue;
                ItemPlayer ip = Instantiate(prefabItemPlayer, parentPointPlayers);
                ip.StartThis(match.teamA.players[i], true, showingNamesPoint);
                itemsPlayerPoint.Add(ip);
            }
        }
        else
        {
            for (int i = 0; i < match.teamB.players.Count; i++)
            {
                if (currentItemPlayer.GetPlayer().ID == match.teamB.players[i].ID) continue;
                ItemPlayer ip = Instantiate(prefabItemPlayer, parentPointPlayers);
                ip.StartThis(match.teamB.players[i], false, showingNamesPoint);
                itemsPlayerPoint.Add(ip);
            }
        }
    }

    public void ButtonPlayerInfoPoint()
    {
        showingNamesPoint = !showingNamesPoint;

        buttonPlayersInfoPoint.sprite = spritesButtonPlayerInfo[showingNamesPoint ? 1 : 0];

        foreach (ItemPlayer ip in itemsPlayerPoint)
            ip.SetText(showingNamesPoint);
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

    public void ButtonBackFromEvent()
    {
        isPoint = false;
        isAssist = false;
        ManagerUI.MUI.OpenLayout(ManagerUI.MUI.playingEvent);
    }

    private void AddMatchEvent(MatchEventType met, Player assist = new Player())
    {
        match.events.Add(new MatchEvent()
        {
            gameTime = eventExactTime,
            eventType = met,
            teamA = currentItemPlayer.GetTeamA(),
            playerMain = currentItemPlayer.GetPlayer(),
            playerAssist = assist
        });

        if (met == MatchEventType.POINT || met == MatchEventType.CALLAHAN)
        {
            UpdateScore();
            timerExtra = true;

            if (currentItemPlayer.GetTeamA())
            {
                if (match.GetScoreTotal() > 1)
                {
                    genderPullAPrevious.sprite = ManagerUI.MUI.spritesGenders[Convert.ToInt32(PullGenderFemale(match.GetScoreTotal() - 1))];
                    genderPullAPrevious.color = ManagerUI.MUI.colorsGenders[Convert.ToInt32(PullGenderFemale(match.GetScoreTotal() - 1))];
                    genderPullAPrevious.color = new Color(genderPullAPrevious.color.r, genderPullAPrevious.color.g, genderPullAPrevious.color.b, 0.5f);
                    genderPullAPrevious.gameObject.SetActive(true);
                }
                genderPullA.sprite = ManagerUI.MUI.spritesGenders[Convert.ToInt32(PullGenderFemale(match.GetScoreTotal()))];
                genderPullA.color = ManagerUI.MUI.colorsGenders[Convert.ToInt32(PullGenderFemale(match.GetScoreTotal()))];
                genderPullA.gameObject.SetActive(true);
                genderPullANext.sprite = ManagerUI.MUI.spritesGenders[Convert.ToInt32(PullGenderFemale(match.GetScoreTotal() + 1))];
                genderPullANext.color = ManagerUI.MUI.colorsGenders[Convert.ToInt32(PullGenderFemale(match.GetScoreTotal() + 1))];
                genderPullANext.color = new Color(genderPullANext.color.r, genderPullANext.color.g, genderPullANext.color.b, 0.5f);
                genderPullANext.gameObject.SetActive(true);
            }
            else
            {
                if (match.GetScoreTotal() > 1)
                {
                    genderPullBPrevious.sprite = ManagerUI.MUI.spritesGenders[Convert.ToInt32(PullGenderFemale(match.GetScoreTotal() - 1))];
                    genderPullBPrevious.color = ManagerUI.MUI.colorsGenders[Convert.ToInt32(PullGenderFemale(match.GetScoreTotal() - 1))];
                    genderPullBPrevious.color = new Color(genderPullBPrevious.color.r, genderPullBPrevious.color.g, genderPullBPrevious.color.b, 0.5f);
                    genderPullBPrevious.gameObject.SetActive(true);
                }
                genderPullB.sprite = ManagerUI.MUI.spritesGenders[Convert.ToInt32(PullGenderFemale(match.GetScoreTotal()))];
                genderPullB.color = ManagerUI.MUI.colorsGenders[Convert.ToInt32(PullGenderFemale(match.GetScoreTotal()))];
                genderPullB.gameObject.SetActive(true);
                genderPullBNext.sprite = ManagerUI.MUI.spritesGenders[Convert.ToInt32(PullGenderFemale(match.GetScoreTotal() + 1))];
                genderPullBNext.color = ManagerUI.MUI.colorsGenders[Convert.ToInt32(PullGenderFemale(match.GetScoreTotal() + 1))];
                genderPullBNext.color = new Color(genderPullBNext.color.r, genderPullBNext.color.g, genderPullBNext.color.b, 0.5f);
                genderPullBNext.gameObject.SetActive(true);
            }
        }
    }

    private void AddMatchTimeout(bool _teamA, bool _spirit)
    {
        match.events.Add(new MatchEvent()
        {
            gameTime = eventExactTime,
            eventType = _spirit ? MatchEventType.SPIRIT_TIMEOUT : MatchEventType.TIMEOUT,
            teamA = _teamA,
            playerMain = new Player(),
            playerAssist = new Player()
        });
    }

    private void UpdateGameTime()
    {
        gameTime = (DateTime.Now - startTime) - stoppedTime;
        textTimer.text = (gameTime.Hours > 0 ? gameTime.Hours.ToString("00") + ":" : string.Empty) + gameTime.Minutes.ToString("00") + ":" + gameTime.Seconds.ToString("00");
    }

    private Color TimeToPullColor(float _timer)
    {
        if (_timer > 2 * timeToPullRed) ButtonTimerExtra();
        if (_timer > timeToPullRed) return Color.red;
        if (_timer > timeToPullYellow)
        {
            if (!imageWhistle.activeSelf) imageWhistle.SetActive(true);
            return Color.yellow;
        }
        return Color.green;
    }

    private bool PullGenderFemale(int _point)
    {
        return firstPullFemale == Convert.ToBoolean(Mathf.FloorToInt((_point - 1) / 2) % 2);
    }

    private void UpdateScore()
    {
        textScore.text = match.GetScore(true) + "-" + match.GetScore(false);
    }

    private void UpdateInfo()
    {
        textHalfTime.text = match.halfTime.Hours.ToString("00") + ":" + match.halfTime.Minutes.ToString("00") + ":" + match.halfTime.Seconds.ToString("00");
        textFullTime.text = match.fullTime.Hours.ToString("00") + ":" + match.fullTime.Minutes.ToString("00") + ":" + match.fullTime.Seconds.ToString("00");
    }

    private void Language()
    {
        textInfoTitle.text = ManagerLanguages.ML.Translate("MatchInfo");
        textLabelHalfTime.text = ManagerLanguages.ML.Translate("Halftime");
        textLabelFullTime.text = ManagerLanguages.ML.Translate("Fulltime");
        textLabelFirstPullGender.text = ManagerLanguages.ML.Translate("FirstPull");
        textButtonUndo.text = ManagerLanguages.ML.Translate("Undo");
        textButtonTimeout.text = ManagerLanguages.ML.Translate("Timeout");
        textCalledBy.text = ManagerLanguages.ML.Translate("CalledBy");
        textCalledBySpirit.text = ManagerLanguages.ML.Translate("CalledBy");
        textButtonSpiritTimeout.text = ManagerLanguages.ML.Translate("SpiritTimeout");
        textButtonEnd.text = ManagerLanguages.ML.Translate("End");
        textButtonPoint.text = ManagerLanguages.ML.Translate("Point");
        textButtonAssist.text = ManagerLanguages.ML.Translate("Assistance");
        textButtonDefense.text = ManagerLanguages.ML.Translate("Defense");
        textButtonCallahan.text = ManagerLanguages.ML.Translate("Callahan");
        textExitTitle.text = ManagerLanguages.ML.Translate("Exit");
        textSaveMatch.text = ManagerLanguages.ML.Translate("SaveMatch");
        textDiscardMatch.text = ManagerLanguages.ML.Translate("DiscardMatch");
    }
}
