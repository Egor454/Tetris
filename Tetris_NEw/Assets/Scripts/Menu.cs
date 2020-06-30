using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("NumberOfPlayers");
    }
    public void Control()
    {
        SceneManager.LoadScene("Control");
    }
}
