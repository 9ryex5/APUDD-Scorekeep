using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Match : MonoBehaviour
{
    public TextMeshProUGUI textTimer;
    public TextMeshProUGUI textScore;

    private DateTime startTime;
    private TimeSpan stoppedTime;
    private DateTime startStoppage;
    private int scoreA, scoreB;

    public TextMeshProUGUI textTeamA, textTeamB;
    private string nameA, nameB;
    public Transform parentA, parentB;
    public ItemPlayer prefabItemPlayer;
    private List<ItemPlayer> itemsPlayer;

    public Button buttonStartTime;
    public Sprite[] spritesButtonStartTime;

    public Button buttonPlayersInfo;
    public Sprite[] spritesButtonPlayerInfo;
    private bool showingNames;

    public Team teamA = new Team
    {
        name = "TeamA",
        players = new List<Player>(){
        new Player{ID = 0, name = "Player0", number = 0, female = false},
        new Player{ID = 1, name = "Player1", number = 1, female = false},
        new Player{ID = 2, name = "Player2", number = 2, female = false},
        new Player{ID = 3, name = "Player3", number = 3, female = false},
        new Player{ID = 4, name = "Player4", number = 96, female = true},
        new Player{ID = 5, name = "Player5", number = 97, female = true},
        new Player{ID = 6, name = "Player6", number = 98, female = true},
        new Player{ID = 7, name = "Player7", number = 99, female = true},}
    };

    public Team teamB = new Team
    {
        name = "TeamB",
        players = new List<Player>(){
        new Player{ID = 8, name = "Player8", number = 0, female = false},
        new Player{ID = 9, name = "Player9", number = 1, female = false},
        new Player{ID = 10, name = "Player10", number = 2, female = false},
        new Player{ID = 11, name = "Player11", number = 3, female = false},
        new Player{ID = 12, name = "Player12", number = 93, female = true},
        new Player{ID = 13, name = "Player13", number = 92, female = true},
        new Player{ID = 14, name = "Player14", number = 91, female = true},
        new Player{ID = 15, name = "Player15", number = 90, female = true},}
    };

    private bool matchStarted;
    private bool timeIsRunning;

    private void OnEnable()
    {
        matchStarted = false;
        timeIsRunning = false;
        textTimer.text = "00:00:00";
        UpdateScore();
        SetTeams();
        buttonStartTime.GetComponent<Image>().sprite = spritesButtonStartTime[0];
    }

    private void Update()
    {
        if (!timeIsRunning) return;

        TimeSpan timePassed = (DateTime.Now - startTime) - stoppedTime;
        textTimer.text = timePassed.Hours.ToString("00") + ":" + timePassed.Minutes.ToString("00") + ":" + timePassed.Seconds.ToString("00");
    }

    private void SetTeams()
    {
        for (int i = 0; i < parentA.childCount; i++)
            Destroy(parentA.GetChild(i).gameObject);

        for (int i = 0; i < parentB.childCount; i++)
            Destroy(parentB.GetChild(i).gameObject);

        textTeamA.text = teamA.name;
        textTeamB.text = teamB.name;

        itemsPlayer = new List<ItemPlayer>();

        for (int i = 0; i < teamA.players.Count; i++)
        {
            ItemPlayer ip = Instantiate(prefabItemPlayer, parentA);
            ip.StartThis(teamA.players[i]);
            itemsPlayer.Add(ip);
        }

        for (int i = 0; i < teamB.players.Count; i++)
        {
            ItemPlayer ip = Instantiate(prefabItemPlayer, parentB);
            ip.StartThis(teamB.players[i]);
            itemsPlayer.Add(ip);
        }
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
            if (timeIsRunning)
                startStoppage = DateTime.Now;
            else
                stoppedTime += DateTime.Now - startStoppage;
        }

        timeIsRunning = !timeIsRunning;
        buttonStartTime.GetComponent<Image>().sprite = spritesButtonStartTime[timeIsRunning ? 1 : 0];
    }

    public void ButtonPlayerInfo()
    {
        showingNames = !showingNames;

        buttonPlayersInfo.GetComponent<Image>().sprite = spritesButtonPlayerInfo[showingNames ? 1 : 0];

        foreach (ItemPlayer ip in itemsPlayer)
            ip.myText.text = showingNames ? ip.GetPlayer().name : ip.GetPlayer().number.ToString();

    }

    private void UpdateScore()
    {
        textScore.text = scoreA + "-" + scoreB;
    }
}
