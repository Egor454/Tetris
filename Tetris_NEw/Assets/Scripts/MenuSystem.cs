using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSystem : MonoBehaviour
{
    [SerializeField] private Text hud_scoreend;//поле для вывода конечных набранных очков на экран
    [SerializeField] private Text hud_linesend;// поле вывода конечных заполненых линий на экран
    private int scoreEnd;
    private float lineEnd;

    public void PlayAgain()//функция которая запускает игру занова
    {
        SceneManager.LoadScene("Level");
        //scoreEnd = GameObject.Find("GameScriptEnd").GetComponent<Game>().CurrentScore;
        //lineEnd = GameObject.Find("GameScriptEnd").GetComponent<Game>().NumLineCleared;
        //Game.gameStarted = false;
    }
     void Update()//функция обновления
    {
        Endscore();
    }
    public void Endscore()//функция вывода конечных результатов на экран
    {
        scoreEnd = GameObject.Find("GameScriptEnd").GetComponent<Game>().CurrentScore;
        lineEnd = GameObject.Find("GameScriptEnd").GetComponent<Game>().NumLineCleared;
        hud_scoreend.text = scoreEnd.ToString();
        hud_linesend.text = lineEnd.ToString();
    }
    public void BackMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}

