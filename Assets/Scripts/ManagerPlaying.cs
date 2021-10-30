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


    private Match match;
    private bool matchStarted;
    private bool timeIsRunning;
    private DateTime startTime;
    private TimeSpan gameTime;
    private TimeSpan stoppedTime;
    private DateTime startStoppage;
    private bool warnedHalfTime, warnedFullTime;
    private bool isPoint;
    private bool isAssist;

    //Teams
    public TextMeshProUGUI textTeamA, textTeamB;
    public Transform parentA, parentB;
    public ItemPlayer prefabItemPlayer;
    private List<ItemPlayer> itemsPlayer, itemsPlayerPoint;

    //Pull Info
    private TimeSpan timeToPull1 = new TimeSpan(0, 0, 75);
    private TimeSpan timeToPull2 = new TimeSpan(0, 0, 15);
    public GameObject goPullInfo;
    public Image genderPullPrevious;
    public Image genderPullCurrent;
    public Image genderPullNext;
    private bool firstPullFemale;
    public TextMeshProUGUI textTimerSincePoint;
    private bool countingPull;
    public GameObject[] imagesPullWhistle;

    //Timeout Info
    private TimeSpan timeToTimeout1 = new TimeSpan(0, 0, 75);
    private TimeSpan timeToTimeout2 = new TimeSpan(0, 0, 15);
    public GameObject goTimeoutInfo;
    public TextMeshProUGUI labelTimeout;
    public TextMeshProUGUI textTimerSinceTimeout;
    private bool countingTimeout;
    public GameObject[] imagesTimeoutWhistle;
    private bool timeoutBeforePull;

    //Settings
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
    public GameObject goInfo;
    public TextMeshProUGUI textInfoTitle;
    public TextMeshProUGUI textLabelHalfTime, textLabelFullTime;
    public TextMeshProUGUI textHalfTime, textFullTime;
    public TextMeshProUGUI textToHalfTime, textToFullTime;
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
    private TimeSpan eventExactTime, possibleEventExactTime;
    private TimeSpan timeLastPoint;
    private TimeSpan timeLastTimeout;

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

        UpdateGameTime();

        if (countingTimeout)
        {
            float timerTimeout = (float)(timeToTimeout1 - (gameTime - timeLastTimeout)).TotalSeconds;
            textTimerSinceTimeout.text = Mathf.FloorToInt(timerTimeout).ToString();

            if (timerTimeout < 0 && !imagesTimeoutWhistle[0].activeSelf) imagesTimeoutWhistle[0].SetActive(true);
            if (timerTimeout < 0 - timeToTimeout2.TotalSeconds && !imagesTimeoutWhistle[1].activeSelf) imagesTimeoutWhistle[1].SetActive(true);
            if (timerTimeout < 0 - timeToTimeout1.TotalSeconds) ButtonStopCountingTimeout();
        }

        if (countingPull)
        {
            float timerPull = (float)(timeToPull1 - (gameTime - timeLastPoint) + (timeoutBeforePull ? timeToTimeout1 : TimeSpan.Zero)).TotalSeconds;
            textTimerSincePoint.text = Mathf.FloorToInt(timerPull).ToString();

            if (timerPull < 0 && !imagesPullWhistle[0].activeSelf) imagesPullWhistle[0].SetActive(true);
            if (timerPull < 0 - timeToPull2.TotalSeconds && !imagesPullWhistle[1].activeSelf) imagesPullWhistle[1].SetActive(true);
            if (timerPull < 0 - timeToPull1.TotalSeconds) ButtonStopCountingPull();
        }

        if (goInfo.activeSelf)
        {
            textToHalfTime.text = (match.halfTime > gameTime ? "-" : "+") + Helpers.TimeSpanToString(match.halfTime - gameTime);
            textToFullTime.text = (match.fullTime > gameTime ? "-" : "+") + Helpers.TimeSpanToString(match.fullTime - gameTime);
        }

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
        textTimer.text = Helpers.TimeSpanToString(TimeSpan.Zero, true);
        warnedHalfTime = false;
        warnedFullTime = false;
        UpdateScore();
        ButtonFirstGenderPull(firstPullFemale);
        goPullInfo.SetActive(false);
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
        goPullInfo.SetActive(false);

        itemsPlayer = new List<ItemPlayer>();

        for (int i = 0; i < match.teamA.players.Count; i++)
        {
            ItemPlayer ip = Instantiate(prefabItemPlayer, parentA);
            ip.StartThis(match.teamA.players[i], true, true);
            itemsPlayer.Add(ip);
        }

        for (int i = 0; i < match.teamB.players.Count; i++)
        {
            ItemPlayer ip = Instantiate(prefabItemPlayer, parentB);
            ip.StartThis(match.teamB.players[i], false, true);
            itemsPlayer.Add(ip);
        }
    }

    public void ButtonFirstGenderPull(bool _female)
    {
        firstPullFemale = _female;
        imageFirstPullFemale.color = new Color(imageFirstPullFemale.color.r, imageFirstPullFemale.color.g, imageFirstPullFemale.color.b, _female ? 1 : 0.5f);
        imageFirstPullMale.color = new Color(imageFirstPullMale.color.r, imageFirstPullMale.color.g, imageFirstPullMale.color.b, _female ? 0.5f : 1);
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

    public void ButtonStopCountingTimeout()
    {
        countingTimeout = false;
        goTimeoutInfo.SetActive(false);
    }

    public void ButtonStopCountingPull()
    {
        countingPull = false;
        goPullInfo.SetActive(false);
        timeoutBeforePull = false;
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
        eventExactTime = possibleEventExactTime;
        AddMatchTimeout(_teamA, false);
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

        SaveData.SD.AddMatch(match);
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

            eventExactTime = gameTime;
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
            countingPull = true;
            timeLastPoint = eventExactTime;

            genderPullPrevious.sprite = ManagerUI.MUI.spritesGenders[Convert.ToInt32(PullGenderFemale(match.GetScoreTotal() - 1))];
            genderPullPrevious.color = ManagerUI.MUI.colorsGenders[Convert.ToInt32(PullGenderFemale(match.GetScoreTotal() - 1))];
            genderPullPrevious.color = new Color(genderPullPrevious.color.r, genderPullPrevious.color.g, genderPullPrevious.color.b, 0.5f);
            genderPullCurrent.sprite = ManagerUI.MUI.spritesGenders[Convert.ToInt32(PullGenderFemale(match.GetScoreTotal()))];
            genderPullCurrent.color = ManagerUI.MUI.colorsGenders[Convert.ToInt32(PullGenderFemale(match.GetScoreTotal()))];
            genderPullNext.sprite = ManagerUI.MUI.spritesGenders[Convert.ToInt32(PullGenderFemale(match.GetScoreTotal() + 1))];
            genderPullNext.color = ManagerUI.MUI.colorsGenders[Convert.ToInt32(PullGenderFemale(match.GetScoreTotal() + 1))];
            genderPullNext.color = new Color(genderPullNext.color.r, genderPullNext.color.g, genderPullNext.color.b, 0.5f);

            RectTransform rt = goPullInfo.GetComponent<RectTransform>();

            if (match.events.Last().teamA)
            {
                rt.anchorMin = new Vector2(0.1f, rt.anchorMin.y);
                rt.anchorMax = new Vector2(0.35f, rt.anchorMax.y);
            }
            else
            {
                rt.anchorMin = new Vector2(0.65f, rt.anchorMin.y);
                rt.anchorMax = new Vector2(0.9f, rt.anchorMax.y);
            }

            rt.anchoredPosition = Vector2.zero;
            goPullInfo.SetActive(true);
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

        countingTimeout = true;
        timeLastTimeout = eventExactTime;
        goTimeoutInfo.SetActive(true);
        if (countingPull) timeoutBeforePull = true;
    }

    private void UpdateGameTime()
    {
        gameTime = (DateTime.Now - startTime) - stoppedTime;
        textTimer.text = Helpers.TimeSpanToString(gameTime, true);
    }

    private bool PullGenderFemale(int _point)
    {
        if (_point == 0) return firstPullFemale;
        return firstPullFemale == Convert.ToBoolean(Mathf.FloorToInt((_point - 1) / 2) % 2);
    }

    private void UpdateScore()
    {
        textScore.text = match.GetScore(true) + "-" + match.GetScore(false);
    }

    private void UpdateInfo()
    {
        textHalfTime.text = Helpers.TimeSpanToString(match.halfTime);
        textFullTime.text = Helpers.TimeSpanToString(match.fullTime);
    }

    private void Language()
    {
        textInfoTitle.text = ManagerLanguages.ML.Translate("MatchInfo");
        textLabelHalfTime.text = ManagerLanguages.ML.Translate("Halftime");
        textLabelFullTime.text = ManagerLanguages.ML.Translate("Fulltime");
        textLabelFirstPullGender.text = ManagerLanguages.ML.Translate("FirstPull");
        labelTimeout.text = ManagerLanguages.ML.Translate("Timeout");
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
