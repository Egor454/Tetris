using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTetromino : MonoBehaviour
{
    private Game game;
    private Transform followTetrominoTransform;
    void Start()
    {
        foreach (Transform mino in transform)
        {
            mino.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .5f);
        }
    }
    public void InitializeGhost(Game game,Tetromino tetro)
    {
        this.game = game;
        followTetrominoTransform = tetro.transform;
        if (game.PlayerNumber == 1)
        {
            tag = "GhostTetromino1Player";
        }
        if (game.PlayerNumber == 2)
        {
            tag = "GhostTetromino2Player";
        }
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
