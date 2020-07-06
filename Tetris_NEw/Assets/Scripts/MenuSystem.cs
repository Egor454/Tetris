using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSystem : MonoBehaviour
{
    [SerializeField] private Text hud_scoreend;//поле для вывода конечных набранных очков на экран
    [SerializeField] private Text hud_linesend;// поле вывода конечных заполненых линий на экран
    [SerializeField] private Text hud_scoreend2;//поле для вывода конечных набранных очков на экран
    [SerializeField] private Text hud_linesend2;// поле вывода конечных заполненых линий на экран

    private int scoreEnd;
    private int lineEnd;
    private int playerOne;
    private int playerTwo;
    public void PlayAgain()//функция которая запускает игру занова
    {
        SceneManager.LoadScene("Level");
        GlobalScore.Instance.Restrat();
    }
    void Awake()//функция обновления
    {
        Endscore();
    }
    public void Endscore()//функция вывода конечных результатов на экран
    {
        if (GlobalScore.Instance.NumberPlayers == 0)
        {
            hud_scoreend.text = GlobalScore.Instance.ScoreEnd.ToString();
            hud_linesend.text = GlobalScore.Instance.LineEnd.ToString();

        }
        else if (GlobalScore.Instance.NumberPlayers == 1)
        {
            hud_scoreend.text = GlobalScore.Instance.ScoreEnd.ToString();
            hud_linesend.text = GlobalScore.Instance.LineEnd.ToString();
            hud_scoreend2.text = GlobalScore.Instance.ScoreEnd2.ToString();
            hud_linesend2.text = GlobalScore.Instance.LineEnd2.ToString();
        }
    }
    public void BackMenu()
    {
        SceneManager.LoadScene("Menu");
        GlobalScore.Instance.Restrat();
    }
}

