using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalScore : GlobalResultManager<GlobalScore>
{
    [SerializeField] private int scoreEnd;
    public int ScoreEnd=>scoreEnd;

    [SerializeField] private float lineEnd;
    public float LineEnd=>lineEnd;
    public void InsertScore(int scoreend, float lineend)
    {
        scoreEnd = scoreend;
        lineEnd = lineend;
    }

}
