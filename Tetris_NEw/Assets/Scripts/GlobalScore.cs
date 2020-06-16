using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalScore : MonoBehaviour
{
    [SerializeField] private int scoreEnd;
    public int ScoreEnd=>scoreEnd;

    [SerializeField] private float lineEnd;
    public float LineEnd=>lineEnd;
    public void InsertScore()
    {
        scoreEnd = GameObject.Find("GameScript").GetComponent<Game>().CurrentScore;
        lineEnd = GameObject.Find("GameScript").GetComponent<Game>().NumLineCleared;
    }

}
