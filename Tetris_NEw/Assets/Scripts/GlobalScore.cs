using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalScore : MonoBehaviourSingleton<GlobalScore>
{
    private int scoreEnd;
    private int playerNumbers;
    private int numberPlayers;
    private int lineEnd;
    private int lineEnd2;
    private int scoreEnd2;
    private int playersFinished;

    public int ScoreEnd => scoreEnd;
    public int NumberPlayers => numberPlayers;
    public int LineEnd => lineEnd;
    public int LineEnd2 => lineEnd2;
    public int ScoreEnd2 => scoreEnd2;
    public int PlayersFinished => playersFinished;
    public int PlayerNumbers => playerNumbers;

    public void InsertScore(int scoreend, int lineend, int playernumber)
    {
        if (playernumber == 1)
        {
            scoreEnd = scoreend;
            lineEnd = lineend;
        }
        else if (playernumber == 2)
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
