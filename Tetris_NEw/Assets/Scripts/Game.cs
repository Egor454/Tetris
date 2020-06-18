using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
   private  int gridWidth = 10;// ширина поле игры
   private int gridHeight = 20;//высота поля игры
   private Transform[,] grid;// обновленное поле игры,с учетом наличие на ней фигур 

    [SerializeField] private int scoreOneLine = 60;//очки которые прибавляются за 1 собранную линию
    [SerializeField] private int scoreTwoLine = 120;//очки которые прибавляются за 2 собранную линию
    [SerializeField] private int scoreThreeLine = 320;//очки которые прибавляются за 3 собранную линию
    [SerializeField] private int scoreFourLine = 1400;//очки которые прибавляются за 4 собранную линию

    [SerializeField] private float fallSpeed=1.0f;//скорость падения фигур вниз
    public float FallSpeed => fallSpeed;

    [SerializeField]  private Text hud_score;//поле для вывода очков
    [SerializeField]  private Text hud_speed;//поле для вывода скорости
    [SerializeField]  private Text hud_lines;//поле для вывода линий заполненных

    private  int numberOfRowsThisTurn=0;//количество заполненных линий одновременно 

    [SerializeField] private int currentScore = 0;//очки игрока
    public int CurrentScore => currentScore;
    [SerializeField] private bool gameOver=false;
    public bool Gameover => gameOver;

    [SerializeField] private int individualScore = 100;//бонусные очки которые начисляются  если быстро опустить фигуры вниз
    private float individualScoreTime;// таймер для бонусных очков

    private float currentLevel = 0;// уровень игры который меняет скорость падения фигур ,в зависимости от количества заполненных линий
    [SerializeField] private float numLineCleared=0;// количество заполненных линий всего
    public float NumLineCleared => numLineCleared;

    private  GameObject previewTetromino;// показывает следующую фигуру 
    private  GameObject nextTetromino;// фигура которая появляется для управления

    private  bool gameStarted = false;// началась игра или нет

    private Vector2 previewTetrominoPosition = new Vector2(15.0f, 15);// расположение следующей фигуры на экране

    void Start()// старт игры
    {
        SpawnNextTetromino();
    }

    //Update is called once per frame
    void Update()
    {
        UpdateSpeedPlayer();
        UpdateScore();
        UpdateUI();
    }
    void UpdateSpeedPlayer()//функция которая отлавливает нажатия игрока на + и -, и в зависимости от нажатой кнопки меняет скорость падения фигур вниз
    {
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            if (fallSpeed >= 0)
            {
                fallSpeed = fallSpeed - 0.1f;
            }
            else
            {
                
            }
            
        }
        else if(Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            if (fallSpeed <= 2)
            {
                fallSpeed = fallSpeed + 0.1f;
            }
            else
            {
                
            }
        }
    }
    void UpdateLevel()// функция для обновления уровня 
    {

        currentLevel = numLineCleared / 10;
        Debug.Log("Current Level:" + currentLevel);
    }
    void UpdateSpeed()//функция меняет скорость падения фигур вниз в зависимости от уровня
    {
        if (fallSpeed >= 0)
        {
            fallSpeed = fallSpeed - (currentLevel * 0.4f);
            Debug.Log("Current Fall Speed:" + fallSpeed);
        }
        else
        {

        }
    }
    public void UpdateUI()// функция для вывода значений(очков,уровня,линий) на экран
    {
        if (fallSpeed >-1 && fallSpeed<0.00000001)
        {
            hud_speed.text = 0.ToString();
        }
        else
        {
            hud_speed.text = fallSpeed.ToString();
        }
        hud_score.text = currentScore.ToString();
        hud_lines.text = numLineCleared.ToString();
    }
    public void UpdateIndividualScore()//присвоение бонусных очков, чем дольше падает фигура тем меньше бонусных очков 
    {
        currentScore += individualScore;
        individualScore = 100;
    }
    public void UpdateScore()//функция для обновления очков в зависимости от количества заполненных линий
    {
        if (individualScoreTime < 1)
        {
            individualScoreTime += Time.deltaTime;
        }
        else
        {
            individualScoreTime = 0;
            individualScore = Mathf.Max(individualScore - 10, 0);
        }
        if (numberOfRowsThisTurn > 0)
        {
            if (numberOfRowsThisTurn == 1)
            {
                ClearedOneLine();
            }
            else if (numberOfRowsThisTurn == 2)
            {
                ClearedTwoLine();
            }
            else if (numberOfRowsThisTurn == 3)
            {
                ClearedThreeLine();
            }
            else if (numberOfRowsThisTurn == 4)
            {
                ClearedFourLine();
            }
            numberOfRowsThisTurn = 0;
        }
    }
    public void ClearedOneLine()//когда заполенна одна линия
    {
        currentScore += scoreOneLine;
        numLineCleared++;
        UpdateLevel();
        UpdateSpeed();
    }
    public void ClearedTwoLine()//когда заполенны две линии
    {
        currentScore += scoreTwoLine;
        numLineCleared += 2;
        UpdateLevel();
        UpdateSpeed();
    }
    public void ClearedThreeLine()//когда заполенны три линии
    {
        currentScore += scoreThreeLine;
        numLineCleared += 3;
        UpdateLevel();
        UpdateSpeed();
    }
    public void ClearedFourLine()//когда заполенны четыре линии
    {
        currentScore += scoreFourLine;
        numLineCleared += 4;
        UpdateLevel();
        UpdateSpeed();
    }
    public bool CheckIsAboveGrid(Tetromino tetromino)//проверяет на окончание игры.Если фигура только появилась и в момент ее перемещения на одну линию вниз она уперлась -проигрыш
    {
        for (int x = 0; x < gridWidth; x++)
        {
            foreach (Transform mino in tetromino.transform)
            {
                Vector2 pos = Round(mino.position);
                if (pos.y > gridHeight - 1)
                {
                    return true;
                }
            }
        }
        return false;
    }
    public bool IsFullRowAt(int y)// проверяет на заполненность линии ,если она заполнена возращает true и прибавляет 1 к переменной  numberOfRowsThisTurn
    {
        for (int x = 0; x < gridWidth; x++)
        {
            if (grid[x, y] == null)
            {
                return false;
            }

        }
        numberOfRowsThisTurn++;
        return true;
    }
    public void DeleteMinoAt(int y)//удаляет заполненную линию 
    {
        for (int x = 0; x < gridWidth; x++)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }
    public void MoveRowDown(int y)// перемещает вниз линию по блокам ,которая была выше удаленной 
    {
        for (int x = 0; x < gridWidth; x++)
        {
            if (grid[x, y] != null)
            {
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;
                grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }
    public void MoveAllRowsDown(int y)// функция которая вызывает функцию MoveRowDown и передает ей знаечние следующей линии
    {
        for (int i = y; i < gridHeight; i++)
        {
            MoveRowDown(i);
        }
    }
    public void DeleteRow()//Функция передает занчение линий для проверки на заполенность, и если линия заполнена вызывает все остальные функции для удалении линии 
    {
        for (int y = 0; y < gridHeight; y++)
        {
            if (IsFullRowAt(y))
            {
                DeleteMinoAt(y);
                MoveAllRowsDown(y + 1);
                y--;
            }
        }
    }
    public void UpdateGrid(Tetromino tetromino)// обновления границ поля
    {
        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                if (grid[x, y] != null)
                {
                    if (grid[x, y].parent == tetromino.transform)
                    {
                        grid[x, y] = null;
                    }
                }
            }
        }
        foreach (Transform mino in tetromino.transform)
        {
            Vector2 pos = Round(mino.position);
            if (pos.y < gridHeight)
            {
                grid[(int)pos.x, (int)pos.y] = mino;
            }
        }
    }
    public Transform GetTransformAtGridPosition(Vector2 pos)//проверка появления фигуры
    {
        //так как мы создаем экземпляр фигуры выше высоты сетки, которая не является частью массива, мы возвращаем null вместо попытки вернуть несуществующее преобразование
        if (pos.y > gridHeight - 1)
        {
            return null;
        }
        //если фигура находится ниже высоты сетки, мы можем вернуть преобразование в позицию
        else 
        {
            return grid[(int)pos.x, (int)pos.y];
        }
    }
    public void SpawnNextTetromino()// спавнит фигуры
    {
        if (!gameStarted)// если игра только началась, создает фигуру для управления и показывает следующую фигуру
        {
            grid = new Transform[gridWidth, gridHeight];
            currentScore = 0;
            numLineCleared = 0;
            gameStarted = true;
            nextTetromino = (GameObject)Instantiate(Resources.Load(GetRandomTetromino(), typeof(GameObject)), new Vector2(5.0f, 20.0f), Quaternion.identity);
            previewTetromino = (GameObject)Instantiate(Resources.Load(GetRandomTetromino(), typeof(GameObject)), previewTetrominoPosition, Quaternion.identity);
            previewTetromino.GetComponent<Tetromino>().enabled = false;
        }
        else//если игра уже идет, следующую фигуру перемещает под управление игрока и показывает следующую фигуру
        {
            previewTetromino.transform.localPosition = new Vector2(5.0f, 20.0f);
            nextTetromino = previewTetromino;
            nextTetromino.GetComponent<Tetromino>().enabled = true;

            previewTetromino = (GameObject)Instantiate(Resources.Load(GetRandomTetromino(), typeof(GameObject)), previewTetrominoPosition, Quaternion.identity);
            previewTetromino.GetComponent<Tetromino>().enabled = false;
        }
    }
    public bool CheckIsInsideGrid(Vector2 pos)// проверка столкновений с полем
    {
        return ((int)pos.x >= 0 && (int)pos.x < gridWidth && (int)pos.y >= 0);
    }
    public Vector2 Round(Vector2 pos)//округляет координаты фигур
    {
        return new Vector2(Mathf.Round(pos.x), Mathf.Round(pos.y));
    }

    string GetRandomTetromino()// рандомное определение следующей фигуры
    {
        int randomTetromino = Random.Range(1, 8);
        string randomTetrominoName = "Prefabs/Tetromino_T";
        switch (randomTetromino)
        {
            case 1:
                randomTetrominoName = "Prefabs/Tetromino_J";
                break;
            case 2:
                randomTetrominoName = "Prefabs/Tetromino_L";
                break;
            case 3:
                randomTetrominoName = "Prefabs/Tetromino_T";
                break;
            case 4:
                randomTetrominoName = "Prefabs/Tetromino_Long";
                break;
            case 5:
                randomTetrominoName = "Prefabs/Tetromino_Square";
                break;
            case 6:
                randomTetrominoName = "Prefabs/Tetromino_S";
                break;
            case 7:
                randomTetrominoName = "Prefabs/Tetromino_Z";
                break;
        }
        return randomTetrominoName;
    }
    public void GameOver()// окончание игры, вызов сцены с результатами 
    {
        gameOver = true;
        GlobalScore.Instance.InsertScore(currentScore, numLineCleared);
        FindObjectOfType<SceneSwap>().UpdateScene();

    }
}
