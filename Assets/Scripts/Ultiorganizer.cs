using System;
using System.Collections.Generic;

public class Ultiorganizer
{
    public Match ImportMatch()
    {
        Team[] pickedTeams = PickTeams();
        return new Match(DateTime.Now, new TimeSpan(0, 30, 0), new TimeSpan(1, 0, 0), pickedTeams[0], pickedTeams[1]);
    }

    private Team[] PickTeams()
    {
        Team[] allTeams = new Team[] {
        new Team{
        myName = "TeamA",
        players = new List<Player>(){
        new Player{ID = 0, myName = "Player 0", number = 0, female = false},
        new Player{ID = 1, myName = "Player 1", number = 1, female = false},
        new Player{ID = 2, myName = "Player 2", number = 2, female = false},
        new Player{ID = 3, myName = "Player 3", number = 3, female = false},
        new Player{ID = 4, myName = "Player 4", number = 4, female = true},
        new Player{ID = 5, myName = "Player 5", number = 5, female = true},
        new Player{ID = 6, myName = "Player 6", number = 6, female = true},
        new Player{ID = 7, myName = "Player 7", number = 7, female = true}}
    },
        new Team{
        myName = "TeamB",
        players = new List<Player>(){
        new Player{ID = 8, myName = "Player 8", number = 8, female = false},
        new Player{ID = 9, myName = "Player 9", number = 9, female = false},
        new Player{ID = 10, myName = "Player 10", number = 10, female = false},
        new Player{ID = 11, myName = "Player 11", number = 11, female = false},
        new Player{ID = 12, myName = "Player 12", number = 12, female = true},
        new Player{ID = 13, myName = "Player 13", number = 13, female = true},
        new Player{ID = 14, myName = "Player 14", number = 14, female = true},
        new Player{ID = 15, myName = "Player 15", number = 15, female = true}}
    },
        new Team{
        myName = "TeamC",
        players = new List<Player>(){
        new Player{ID = 16, myName = "Player 16", number = 16, female = false},
        new Player{ID = 17, myName = "Player 17", number = 17, female = false},
        new Player{ID = 18, myName = "Player 18", number = 18, female = false},
        new Player{ID = 19, myName = "Player 19", number = 19, female = false},
        new Player{ID = 20, myName = "Player 20", number = 20, female = true},
        new Player{ID = 21, myName = "Player 21", number = 21, female = true},
        new Player{ID = 22, myName = "Player 22", number = 22, female = true},
        new Player{ID = 23, myName = "Player 23", number = 23, female = true}}
    },
        new Team{
        myName = "TeamD",
        players = new List<Player>(){
        new Player{ID = 24, myName = "Player 24", number = 24, female = false},
        new Player{ID = 25, myName = "Player 25", number = 25, female = false},
        new Player{ID = 26, myName = "Player 26", number = 26, female = false},
        new Player{ID = 27, myName = "Player 27", number = 27, female = false},
        new Player{ID = 28, myName = "Player 28", number = 28, female = true},
        new Player{ID = 29, myName = "Player 29", number = 29, female = true},
        new Player{ID = 30, myName = "Player 30", number = 30, female = true},
        new Player{ID = 31, myName = "Player 31", number = 31, female = true}}
    }
    };

        Team[] currentTeams = new Team[2];
        int r1 = UnityEngine.Random.Range(0, 4);
        int r2;
        currentTeams[0] = allTeams[r1];
        do
        {
            r2 = UnityEngine.Random.Range(0, 4);
        } while (r1 == r2);
        currentTeams[1] = allTeams[r2];

        return currentTeams;
    }
}