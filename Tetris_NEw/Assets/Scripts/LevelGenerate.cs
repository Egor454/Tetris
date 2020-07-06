using UnityEngine;

public class LevelGenerate : MonoBehaviour
{
    private GameObject singleplayer;
    private GameObject twoplayers;
    [SerializeField] private GameObject prefabGridFirstPlayer;
    [SerializeField] private GameObject prefabGridSecondPlayer;
    [SerializeField] private Camera camera;
    void Start()
    {
        LoadGame();
    }
    public void LoadGame()
    {
        if (GlobalScore.Instance.NumberPlayers == 0)
        {
            singleplayer = Instantiate(prefabGridFirstPlayer, new Vector2(20.0f, 0.0f), Quaternion.identity);
            camera.orthographicSize = 15;


        }
        else
        {
            singleplayer = Instantiate(prefabGridFirstPlayer, new Vector2(20.0f, 0.0f), Quaternion.identity);
            twoplayers = Instantiate(prefabGridSecondPlayer, new Vector2(0.0f, 0.0f), Quaternion.identity);
            camera.orthographicSize = 22;
        }
    }
}
