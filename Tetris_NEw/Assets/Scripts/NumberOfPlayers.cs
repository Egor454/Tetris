using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NumberOfPlayers : MonoBehaviour
{
   public void Back()
    {
        SceneManager.LoadScene("Menu");
    }

    public void SinglePlayer()
    {
        SceneManager.LoadScene("Level");
    }
    public void TwoPlayers()
    {

    }
    public void ThreePlayers()
    {

    }
    public void FourPlayers()
    {

    }

}
