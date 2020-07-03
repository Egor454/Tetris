using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [SerializeField] private int gridWidth = 10;// ширина поле игры
    [SerializeField] private int dinamicGridWidth;
    [SerializeField] private int scoreOneLine = 60;//очки которые прибавляются за 1 собранную линию
    [SerializeField] private int scoreTwoLine = 120;//очки которые прибавляются за 2 собранную линию
    [SerializeField] private int scoreThreeLine = 320;//очки которые прибавляются за 3 собранную линию
    [SerializeField] private int scoreFourLine = 1400;//очки которые прибавляются за 4 собранную линию
    [SerializeField] private Transform locationspawn;
    [SerializeField] private float fallSpeed = 1.0f;//скорость падения фигур вниз
    [SerializeField] private Text hud_score;//поле для вывода очков
    [SerializeField] private Text hud_speed;//поле для вывода скорости
    [SerializeField] private Text hud_lines;//поле для вывода линий заполненных
    [SerializeField] private Vector2 spawnPoint;
    [SerializeField] private Vector2 previewPoint;
    [SerializeField] private int startOfField;
    [SerializeField] private KeyCode downButton;
    [SerializeField] private KeyCode rightButton;
    [SerializeField] private KeyCode leftButton;
    [SerializeField] private KeyCode rotateButton;
    [SerializeField] private Tetromino Tetromino_J;
    [SerializeField] private Tetromino Tetromino_L;
    [SerializeField] private Tetromino Tetromino_T;
    [SerializeField] private Tetromino Tetromino_Long;
    [SerializeField] private Tetromino Tetromino_Square;
    [SerializeField] private Tetromino Tetromino_S;
    [SerializeField] private Tetromino Tetromino_Z;
    [SerializeField] private int currentScore = 0;//очки игрока
    [SerializeField] private bool gameOver = false;
    [SerializeField] private int individualScore = 100;//бонусные очки которые начисляются  если быстро опустить фигуры вниз
    [SerializeField] private float numLineCleared = 0;// количество заполненных линий всего

    private int gridHeight = 20;//высота поля игры
    private Transform[,] grid;// обновленное поле игры,с учетом наличие на ней фигур 
    private float fall = 0;
    private int numberOfRowsThisTurn = 0;//количество заполненных линий одновременно 
    private float individualScoreTime;// таймер для бонусных очков
    private float currentLevel = 0;// уровень игры который меняет скорость падения фигур ,в зависимости от количества заполненных линий
    private Tetromino previewTetromino;// показывает следующую фигуру 
    private Tetromino nextTetromino;// фигура которая появляется для управления
    private Tetromino tetromino;
    private int playersFinished = 0;
    private int playerNumber;
    private bool gameStarted = false;// началась игра или нет
    private int checkingFieldOverflow = 19;
    private int fieldChangeToNormalValue = 20;

    public float FallSpeed => fallSpeed;
    public int CurrentScore => currentScore;
    public bool Gameover => gameOver;
    public float NumLineCleared => numLineCleared;

    void Start()// старт игры
    {
        if ( GlobalScore.Instance.NumberPlayers == 0)
        {
            playerNumber = 1;
        }
        else
        {
            if(GlobalScore.Instance.NumberPlayers == 1 && GlobalScore.Instance.PlayerNumbers==0)
            {
                playerNumber = 1;
                GlobalScore.Instance.InsertPlayerNumber(playerNumber);
            }
            else
            {
                playerNumber = 2;
            }

        }
        SpawnNextTetromino();
    }

    //Update is called once per frame
    void Update()
    {
        PlayerMovement();
        UpdateSpeedPlayer();
        UpdateScore();
        UpdateUI();

    }
    public void Initialize(Tetromino tetromino)
    {
        this.tetromino = tetromino;
    }
    public void InsertFall(float newFall)
    {
        fall = newFall;
    }
    void PlayerMovement()
    {
        if (Input.GetKeyUp(downButton) || Input.GetKeyUp(rightButton) || Input.GetKeyUp(leftButton))
        {
            tetromino.CheckUserInput();
        }
        if (Input.GetKey(downButton) || Time.time - fall >= fallSpeed)
        {
            tetromino.VerticalMovement(nextTetromino);
        }
        else if (Input.GetKey(leftButton))
        {
            tetromino.LeftMovement(nextTetromino);
        }
        else if (Input.GetKeyDown(rotateButton))
        {
            tetromino.Rotation(nextTetromino);
        }
        else if (Input.GetKey(rightButton))
        {
            tetromino.RightMovement(nextTetromino);
        }
    }
    void UpdateSpeedPlayer()//функция которая отлавливает нажатия игрока на + и -, и в зависимости от нажатой кнопки меняет скорость падения фигур вниз
    {
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            if (fallSpeed >= 0)
            {
                fallSpeed = fallSpeed - 0.1f;
            }
        }
        else if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            if (fallSpeed <= 2)
            {
                fallSpeed = fallSpeed + 0.1f;
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
    }
    public void UpdateUI()// функция для вывода значений(очков,уровня,линий) на экран
    {
        if (fallSpeed > -1 && fallSpeed < 0.00000001)
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
                if (pos.x > checkingFieldOverflow)
                    pos.x = pos.x - fieldChangeToNormalValue;
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
            if (pos.x > checkingFieldOverflow)
                pos.x = pos.x - fieldChangeToNormalValue;
            if (pos.y < gridHeight)
            {
                grid[(int)pos.x, (int)pos.y] = mino;
            }
        }
    }
    public Transform GetTransformAtGridPosition(Vector2 pos)//проверка появления фигуры
    {
        if (pos.x > checkingFieldOverflow)
            pos.x = pos.x - fieldChangeToNormalValue;
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
            previewTetromino = Instantiate(GetRandomTetromino(), previewPoint, Quaternion.identity, locationspawn);
            nextTetromino = Instantiate(GetRandomTetromino(), spawnPoint, Quaternion.identity, locationspawn);
            previewTetromino.GetComponent<Tetromino>().enabled = false;
            nextTetromino.Initialize(this);

        }
        else//если игра уже идет, следующую фигуру перемещает под управление игрока и показывает следующую фигуру
        {
            previewTetromino.transform.position = spawnPoint;
            nextTetromino = previewTetromino;
            nextTetromino.GetComponent<Tetromino>().enabled = true;
            previewTetromino = Instantiate(GetRandomTetromino(), previewPoint, Quaternion.identity, locationspawn);
            previewTetromino.GetComponent<Tetromino>().enabled = false;
            nextTetromino.Initialize(this);

        }
    }
    public bool CheckIsInsideGrid(Vector2 pos)// проверка столкновений с полем
    {
        return ((int)pos.x >= startOfField && (int)pos.x < dinamicGridWidth && (int)pos.y >= 0);
    }
    public Vector2 Round(Vector2 pos)//округляет координаты фигур
    {
        return new Vector2(Mathf.Round(pos.x), Mathf.Round(pos.y));
    }
    Tetromino GetRandomTetromino()// рандомное определение следующей фигуры
    {
        int randomTetromino = Random.Range(1, 8);
        Tetromino randomTetrominoName = Tetromino_J;
        switch (randomTetromino)
        {
            case 1:
                randomTetrominoName = Tetromino_J;
                break;
            case 2:
                randomTetrominoName = Tetromino_L;
                break;
            case 3:
                randomTetrominoName = Tetromino_T;
                break;
            case 4:
                randomTetrominoName = Tetromino_Long;
                break;
            case 5:
                randomTetrominoName = Tetromino_Square;
                break;
            case 6:
                randomTetrominoName = Tetromino_S;
                break;
            case 7:
                randomTetrominoName = Tetromino_Z;
                break;
        }
        return randomTetrominoName;
    }
    public void GameOver()// окончание игры, вызов сцены с результатами 
    {
        gameOver = true;
        playersFinished++;
        GlobalScore.Instance.InsertPlayersFinished(playersFinished);
        GlobalScore.Instance.InsertScore(currentScore, numLineCleared, playerNumber);
        this.enabled = false;
        FindObjectOfType<SceneSwap>().UpdateScene(gameOver);
    }
}
