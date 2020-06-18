using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneSwap : MonoBehaviour
{
    private bool gameoverchek;
    public void UpdateScene()
    {
        gameoverchek = GameObject.Find("GameScript").GetComponent<Game>().Gameover;

        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "Level")
        {
            if (gameoverchek == true)
            {
                SceneManager.LoadScene("GameOver");
            }
        }
        else
        {



        }
    }
}
