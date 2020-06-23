using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGanerate : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject singleplayer;
    private GameObject twoplayers;
    void Start()
    {
        LoadGame();
    }
    public void LoadGame()
    {
        if (GlobalScore.Instance.NumberPlayers == 0)
        {
            singleplayer = (GameObject)Instantiate(Resources.Load(LoadSingleLevel(), typeof(GameObject)), new Vector2(0.0f, 0.0f), Quaternion.identity);
        }
        else
        {
            twoplayers = (GameObject)Instantiate(Resources.Load(LoadSingleLevel(), typeof(GameObject)), new Vector2(-18.0f, 0.0f), Quaternion.identity);
            singleplayer = (GameObject)Instantiate(Resources.Load(LoadSingleLevel(), typeof(GameObject)), new Vector2(0.0f, 0.0f), Quaternion.identity);
        }
    }
    string LoadSingleLevel()
    {
        string LevelSingleName = "Prefabs/SinglePlayer";
        return LevelSingleName;
    }
    string LoadTwoLevel()
    {
        string LevelTwoName = "Prefabs/TwoPlayer ";
        return LevelTwoName;
    }
}
