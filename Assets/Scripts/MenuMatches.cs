using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using TMPro;

public class MenuMatches : MonoBehaviour
{
    public static MenuMatches MM;

    public TextMeshProUGUI textTitle;
    public Transform parentMatches;
    public ItemMatch prefabItemMatch;

    public TextMeshProUGUI textMatchTitle, textMatchTitleOptions;
    public Transform parentMatchEvents;
    public ItemMatchEvent prefabMatchEvent;
    private int currentMatchIndex;

    //Options
    public TextMeshProUGUI textDay;
    public TextMeshProUGUI textHour;

    //MatchEvent
    public TextMeshProUGUI textEventTitle;
    public TextMeshProUGUI textEventMain;
    public TextMeshProUGUI textEventPoint, textEventAssist;
    public TMP_InputField fieldHour, fieldMinute, fieldSecond;
    public GameObject goNextTeam, goPreviousTeam;
    public GameObject goDeleteEvent;
    public GameObject goButtonEditMain, goButtonEditPoint, goButtonEditAssist;
    public TextMeshProUGUI textDeleteTitle;
    public TextMeshProUGUI textConfirm;
    public TextMeshProUGUI textCancel;
    private int currentMatchEventIndex;
    public TextMeshProUGUI textEditPlayerTitle;
    public Transform parentPlayers;
    public ItemPlayer prefabItemPlayer;
    private bool editingAssist;
    private List<ItemPlayer> playersEdit;
    public Image buttonPlayersInfo;
    public Sprite[] spritesButtonPlayerInfo;
    private bool showingNames;

    private void Awake()
    {
        MM = this;
    }

    private void OnEnable()
    {
        for (int i = 0; i < parentMatches.childCount; i++)
            Destroy(parentMatches.GetChild(i).gameObject);

        for (int i = 0; i < SaveData.SD.matches.Count; i++)
        {
            ItemMatch im = Instantiate(prefabItemMatch, parentMatches);
            im.StartThis(i);
        }

        Language();
    }

    public void OpenMatch(int _matchIndex)
    {
        Match m = SaveData.SD.matches[_matchIndex];
        currentMatchIndex = _matchIndex;
        textMatchTitle.text = m.teamA.myName + " | " + m.teamB.myName;
        textMatchTitleOptions.text = m.teamA.myName + " | " + m.teamB.myName;

        for (int i = 0; i < parentMatchEvents.childCount; i++)
            Destroy(parentMatchEvents.GetChild(i).gameObject);

        for (int i = 0; i < m.events.Count; i++)
        {
            ItemMatchEvent ime = Instantiate(prefabMatchEvent, parentMatchEvents);
            ime.StartThis(_matchIndex, i, m.GetScore(true, i), m.GetScore(false, i));
        }

        textDay.text = Helpers.DateTimeToString(m.date, true);
        textHour.text = Helpers.DateTimeToString(m.date, false, false);

        ManagerUI.MUI.OpenLayout(ManagerUI.MUI.matchProfile);
    }

    public void OpenMatchEvent(int _matchEventIndex)
    {
        fieldHour.GetComponent<Image>().color = Color.white;
        fieldMinute.GetComponent<Image>().color = Color.white;
        fieldSecond.GetComponent<Image>().color = Color.white;
        goPreviousTeam.SetActive(false);
        goNextTeam.SetActive(false);
        textEventMain.text = string.Empty;
        textEventPoint.text = string.Empty;
        textEventAssist.text = string.Empty;
        goButtonEditMain.SetActive(false);
        goButtonEditPoint.SetActive(false);
        goButtonEditAssist.SetActive(false);
        buttonPlayersInfo.sprite = spritesButtonPlayerInfo[showingNames ? 1 : 0];

        MatchEvent me = SaveData.SD.matches[currentMatchIndex].events[_matchEventIndex];
        currentMatchEventIndex = _matchEventIndex;
        textEventTitle.text = me.EventTypeString();
        fieldHour.text = me.gameTime.Hours.ToString("00");
        fieldMinute.text = me.gameTime.Minutes.ToString("00");
        fieldSecond.text = me.gameTime.Seconds.ToString("00");

        if (me.eventType == MatchEventType.POINT)
        {
            textEventPoint.text = me.playerMain.Identification();
            textEventAssist.text = me.playerAssist.Identification();
            goButtonEditPoint.SetActive(true);
            goButtonEditAssist.SetActive(true);
        }
        else if (me.eventType == MatchEventType.TIMEOUT || me.eventType == MatchEventType.SPIRIT_TIMEOUT)
        {
            textEventMain.text = me.teamA ? SaveData.SD.matches[currentMatchIndex].teamA.myName : SaveData.SD.matches[currentMatchIndex].teamB.myName;
            goPreviousTeam.SetActive(true);
            goNextTeam.SetActive(true);
        }
        else
        {
            textEventMain.text = me.playerMain.Identification();
            goButtonEditMain.SetActive(true);
        }

        ManagerUI.MUI.OpenLayout(ManagerUI.MUI.matchEvent);
    }

    public void EndEditGameTime()
    {
        int h = int.Parse(fieldHour.text);
        int m = int.Parse(fieldMinute.text);
        int s = int.Parse(fieldSecond.text);
        bool invalid = false;

        if (s < 0 || s > 60)
        {
            fieldSecond.GetComponent<Image>().color = ManagerUI.MUI.colorsRightWrong[1];
            invalid = true;
        }

        if (m < 0 || m > 60)
        {
            fieldMinute.GetComponent<Image>().color = ManagerUI.MUI.colorsRightWrong[1];
            invalid = true;
        }

        if (h < 0)
        {
            fieldHour.GetComponent<Image>().color = ManagerUI.MUI.colorsRightWrong[1];
            invalid = true;
        }

        if (invalid) return;
        SaveData.SD.matches[currentMatchIndex].events[currentMatchEventIndex].gameTime = new TimeSpan(h, m, s);
    }

    public void ButtonChangeEventTeam()
    {
        bool teamA = !SaveData.SD.matches[currentMatchIndex].events[currentMatchEventIndex].teamA;
        SaveData.SD.matches[currentMatchIndex].events[currentMatchEventIndex].teamA = teamA;
        textEventMain.text = teamA ? SaveData.SD.matches[currentMatchIndex].teamA.myName : SaveData.SD.matches[currentMatchIndex].teamB.myName;
    }

    public void ButtonEditPlayers(bool _assist)
    {
        playersEdit = new List<ItemPlayer>();

        for (int i = 0; i < parentPlayers.childCount; i++)
            Destroy(parentPlayers.GetChild(i).gameObject);

        Match m = SaveData.SD.matches[currentMatchIndex];
        MatchEvent me = m.events[currentMatchEventIndex];

        if (me.teamA)
        {
            for (int i = 0; i < m.teamA.players.Count; i++)
            {
                if (me.playerMain.ID == m.teamA.players[i].ID) continue;
                if (me.eventType == MatchEventType.POINT && me.playerAssist.ID == m.teamA.players[i].ID) continue;
                ItemPlayer ip = Instantiate(prefabItemPlayer, parentPlayers);
                ip.StartThis(SaveData.SD.matches[currentMatchIndex].teamA.players[i], true, false, showingNames);
                playersEdit.Add(ip);
            }
        }
        else
        {
            for (int i = 0; i < m.teamB.players.Count; i++)
            {
                if (me.playerMain.ID == m.teamB.players[i].ID) continue;
                if (me.eventType == MatchEventType.POINT && me.playerAssist.ID == m.teamB.players[i].ID) continue;
                ItemPlayer ip = Instantiate(prefabItemPlayer, parentPlayers);
                ip.StartThis(SaveData.SD.matches[currentMatchIndex].teamB.players[i], false, false, showingNames);
                playersEdit.Add(ip);
            }
        }

        editingAssist = _assist;
        ManagerUI.MUI.OpenLayout(ManagerUI.MUI.matchEventEdit);
    }

    public void ClickedPlayer(ItemPlayer _ip)
    {
        if (editingAssist)
            SaveData.SD.matches[currentMatchIndex].events[currentMatchEventIndex].playerAssist = _ip.GetPlayer();
        else
            SaveData.SD.matches[currentMatchIndex].events[currentMatchEventIndex].playerMain = _ip.GetPlayer();

        OpenMatchEvent(currentMatchEventIndex);
    }

    public void ButtonPlayerInfo()
    {
        showingNames = !showingNames;

        buttonPlayersInfo.sprite = spritesButtonPlayerInfo[showingNames ? 1 : 0];

        foreach (ItemPlayer ip in playersEdit)
            ip.SetText(showingNames);
    }

    public void ButtonConfirmDeleteEvent()
    {
        SaveData.SD.matches[currentMatchIndex].events.RemoveAt(currentMatchEventIndex);
        SaveData.SD.SaveMatches();
        OpenMatch(currentMatchIndex);
    }

    public void ButtonBackFromEdit()
    {
        SaveData.SD.SaveMatches();
        OpenMatch(currentMatchIndex);
        ManagerUI.MUI.OpenLayout(ManagerUI.MUI.matchProfile);
    }

    private void Language()
    {
        textTitle.text = ManagerLanguages.ML.Translate("Matches");
        textDeleteTitle.text = ManagerLanguages.ML.Translate("DeleteEvent");
        textConfirm.text = ManagerLanguages.ML.Translate("Confirm");
        textCancel.text = ManagerLanguages.ML.Translate("Cancel");
        textEditPlayerTitle.text = ManagerLanguages.ML.Translate("ChangePlayer");
    }
}
