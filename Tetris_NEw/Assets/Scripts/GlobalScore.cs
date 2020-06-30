using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalScore : MonoBehaviourSingleton<GlobalScore>
{
     private int scoreEnd;
    public int ScoreEnd=>scoreEnd;

     private int numberPlayers;
    public int NumberPlayers => numberPlayers;

     private int lineEnd;
    public int LineEnd =>lineEnd;

    private int lineEnd2;
    public int LineEnd2 => lineEnd2;

    private int scoreEnd2;
    public int ScoreEnd2 => scoreEnd2;

    private int playersFinished;
    public int PlayersFinished => playersFinished;

    private int playerNumbers;
    public int PlayerNumbers => playerNumbers;

    public void InsertScore(int scoreend, int lineend, int playernumber)
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
