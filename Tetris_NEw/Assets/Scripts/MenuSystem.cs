using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSystem : MonoBehaviour
{
    public Text hud_scoreend;//поле для вывода конечных набранных очков на экран
    public Text hud_linesend;// поле вывода конечных заполненых линий на экран
    public void PlayAgain()//функция которая запускает игру занова
    {
        SceneManager.LoadScene("Level");
        Game.numLineCleared = 0;
        Game.currentScore = 0;
        Game.gameStarted = false;
    }
     void Update()//функция обновления
    {
        Endscore();
    }
    public void Endscore()//функция вывода конечных результатов на экран
    {
        hud_scoreend.text = Game.currentScore.ToString();
        hud_linesend.text = Game.numLineCleared.ToString();
    }
}

