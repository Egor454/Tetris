using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalScore : GlobalResultManager<GlobalScore>
{
     private int scoreEnd;
    public int ScoreEnd=>scoreEnd;

     private int numberPlayers;
    public int NumberPlayers => numberPlayers;

     private float lineEnd;
    public float LineEnd=>lineEnd;

    private float lineEnd2;
    public float LineEnd2 => lineEnd2;

    private int scoreEnd2;
    public int ScoreEnd2 => scoreEnd2;

    private float playersFinished;
    public float PlayersFinished => playersFinished;

    private float playerNumbers;
    public float PlayerNumbers => playerNumbers;

    public void InsertScore(int scoreend, float lineend, int playernumber)
    {
        if (playernumber == 1)
        {
            scoreEnd = scoreend;
            lineEnd = lineend;
        }else if(playernumber == 2)
        {
            scoreEnd2 = scoreend;
            lineEnd2 = lineend;
        }
    }
    public void InsertNumberPlayers(int number)
    {
        numberPlayers = number;
    }
    public void InsertPlayersFinished(int finished)
    {
        playersFinished += finished;
    }
    public void InsertPlayerNumber(int playernumber)
    {
        playerNumbers = playernumber;
    }
    public void Restrat()
    {
        playersFinished = 0;
    }
}
