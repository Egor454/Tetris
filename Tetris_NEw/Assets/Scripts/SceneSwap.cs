using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneSwap : MonoBehaviour
{
    
    public void UpdateScene(bool gameoverchek)
    {
        

        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "Level")
        {
            if (gameoverchek == true && GlobalScore.Instance.PlayersFinished==1 && GlobalScore.Instance.NumberPlayers==0)
            {
                SceneManager.LoadScene("GameOver");
            }
            else if(gameoverchek == true && GlobalScore.Instance.PlayersFinished == 2 && GlobalScore.Instance.NumberPlayers == 1)
            {
                SceneManager.LoadScene("GameOverTwo");
            }
        }
        else
        {



        }
    }
}
