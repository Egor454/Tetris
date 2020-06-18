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
    }
     void Update()//функция обновления
    {
        Endscore();
    }
    public void Endscore()//функция вывода конечных результатов на экран
    {

        hud_scoreend.text = GlobalScore.Instance.ScoreEnd.ToString();
        hud_linesend.text = GlobalScore.Instance.LineEnd.ToString();
    }
    public void BackMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}

