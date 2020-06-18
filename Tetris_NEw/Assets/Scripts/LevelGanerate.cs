using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGanerate : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject singleplayer;
    void Start()
    {
        LoadGame();
    }
    public void LoadGame()
    {
        singleplayer = (GameObject)Instantiate(Resources.Load(LoadSingleLevel(), typeof(GameObject)), new Vector2(0.0f, 0.0f), Quaternion.identity);
    }
    string LoadSingleLevel()// рандомное определение следующей фигуры
    {
        string randomLevelName = "Prefabs/SinglePlayer";
        return randomLevelName;
    }
}
