using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTetromino : MonoBehaviour
{
    private Game game;
    private Transform followTetrominoTransform;
    void Start()
    {
        tag = "GhostTetromino";
        foreach (Transform mino in transform)
        {
            mino.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .5f);
        }
        InitializeGhost();
    }
    public void InitializeGhost()
    {
        this.game = GlobalScore.Instance.Games;
        followTetrominoTransform = GlobalScore.Instance.Tettrominos.transform;
    }

    void Update()
    {
        SnapToTetromino();
        MoveDown();
    }
    public void SnapToTetromino()
    {
        transform.position = followTetrominoTransform.position;
        transform.rotation = followTetrominoTransform.rotation;

    }
    void MoveDown()
    {
        while (CheckIsValidPosition())
        {
            transform.position += new Vector3(0, -1, 0);

        }
        if (!CheckIsValidPosition())
        {
            transform.position += new Vector3(0, 1, 0);
        }
    }
    bool CheckIsValidPosition()//  проверка столкновений 
    {
        foreach (Transform mino in transform)
        {
            Vector2 pos = game.Round(mino.position);
            if (game.CheckIsInsideGrid(pos) == false)
            {
                return false;
            }
            if (game.GetTransformAtGridPosition(pos) != null && game.GetTransformAtGridPosition(pos).parent == followTetrominoTransform)
            {
                return true;
            }
            if (game.GetTransformAtGridPosition(pos) != null && game.GetTransformAtGridPosition(pos).parent != transform)
            {
                return false;
            }
        }
        return true;
    }
}
