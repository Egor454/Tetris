﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class NumberOfPlayers : MonoBehaviour
{
    private int playerСhoice;
    public void Back()
    {
        SceneManager.LoadScene("Menu");
    }

    public void SinglePlayer()
    {
        SceneManager.LoadScene("Level");
        playerСhoice = 0;
        GlobalScore.Instance.InsertNumberPlayers(playerСhoice);
    }
    public void TwoPlayers()
    {
        SceneManager.LoadScene("Level");
        playerСhoice = 1;
        GlobalScore.Instance.InsertNumberPlayers(playerСhoice);
    }


}
