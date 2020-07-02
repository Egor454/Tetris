using UnityEngine;

public class Tetromino : MonoBehaviour
{
    [SerializeField] private bool allowRotation = true;// переменная которая позволяет вращать фигуру 
    [SerializeField] private bool limitRotation = false;//переменная для ограничения вращения фигуры,для некторых фигур ограниченно вращение 90 и -90
    [SerializeField] private float continuousVerticalSpeed = 0.05f;//скорость фигуры если нажать и удерживать кнопку вниз
    [SerializeField] private float continuousHorizontalSpeed = 0.1f;//скорость фигуры если нажать и удерживать кнопку влево или вправо
    [SerializeField] private float buttonDownWaitMax = 0.2f;// через какой промежуток времени игра зарегистрирует залипании клавиши 

    private float fall = 0;//таймер обратного отсчета для скорости падения
    private float fallSpeed;// скорость падения 
    private float verticalTimer = 0;//таймер движения вниз
    private float horizontalTimer = 0;//таймер движения по горизонтали
    private float buttonDownWaitTimer = 0;//таймер нажатия клавиши
    private Game game;
    private bool movedImmediateHorizontal = false;//движение по вертикали
    private bool movedImmediateVertical = false; //движения по горизонтали


    public void Initialize(Game game)
    {
        this.game = game;
        game.Initialize(this);
    }
    public void RightMovement(Tetromino tetromino)
    {
        if (movedImmediateHorizontal)
        {
            if (buttonDownWaitTimer < buttonDownWaitMax)
            {
                buttonDownWaitTimer += Time.deltaTime;
                return;
            }
            if (horizontalTimer < continuousHorizontalSpeed)
            {
                horizontalTimer += Time.deltaTime;
                return;
            }
        }
        if (!movedImmediateHorizontal)
        {
            movedImmediateHorizontal = true;
        }
        horizontalTimer = 0;
        tetromino.transform.position += new Vector3(1, 0, 0);// двигаем вправо на 1 позицию 
        if (CheckIsValidPosition())
        {
            game.UpdateGrid(this);
        }
        else
        {
            tetromino.transform.position += new Vector3(-1, 0, 0);// если занято ,возращаем прежнее положение 
        }
    }
    public void LeftMovement(Tetromino tetromino)
    {
        if (movedImmediateHorizontal)
        {
            if (buttonDownWaitTimer < buttonDownWaitMax)
            {
                buttonDownWaitTimer += Time.deltaTime;
                return;
            }
            if (horizontalTimer < continuousHorizontalSpeed)
            {
                horizontalTimer += Time.deltaTime;
                return;
            }
        }
        if (!movedImmediateHorizontal)
        {
            movedImmediateHorizontal = true;
        }
        horizontalTimer = 0;
        tetromino.transform.position += new Vector3(-1, 0, 0);//двигаем влево на 1 позицию
        if (CheckIsValidPosition())
        {
            game.UpdateGrid(this);
        }
        else
        {
            tetromino.transform.position += new Vector3(1, 0, 0);// если занято ,возращаем прежнее положение 
        }

    }
    public void VerticalMovement(Tetromino tetromino)
    {
        if (movedImmediateVertical)
        {
            if (buttonDownWaitTimer < buttonDownWaitMax)
            {
                buttonDownWaitTimer += Time.deltaTime;
                return;
            }
            if (verticalTimer < continuousVerticalSpeed)
            {
                verticalTimer += Time.deltaTime;
                return;
            }
        }
        if (!movedImmediateVertical)
        {
            movedImmediateVertical = true;
        }
        verticalTimer = 0;
        tetromino.transform.position += new Vector3(0, -1, 0);//  двигаем вниз на 1 позицию
        if (CheckIsValidPosition())//проверка столкновений
        {
            game.UpdateGrid(this);// обновляем поле
        }
        else
        {
            tetromino.transform.position += new Vector3(0, 1, 0);// отменяем перемещение
            game.UpdateGrid(this);// обновляем поле
            game.DeleteRow();// проверяем заполнили ли поле
            if (game.CheckIsAboveGrid(this))// если выполнилось условие пройгрыша 
            {
                game.GameOver();// игра окончена
            }
            enabled = false;// запрет на управление этой фигурой
            game.UpdateIndividualScore();
            game.SpawnNextTetromino();// создаем следующую фигуру

        }
        fall = Time.time;
        game.InsertFall(fall);
    }
    public void Rotation(Tetromino tetromino)
    {

        if (allowRotation)//если мы можем вращать фигуру выполняем 
        {
            if (limitRotation)// если ограниченно вращение 
            {
                if (tetromino.transform.rotation.eulerAngles.z >= 90)
                {
                    tetromino.transform.Rotate(0, 0, -90);
                }
                else
                {
                    tetromino.transform.Rotate(0, 0, 90);
                }
            }
            else// иначе вращаем
            {
                tetromino.transform.Rotate(0, 0, 90);
            }
            if (!CheckIsValidPosition())//проверяем столкновения с другими блками и фигурами 
            {
                tetromino.transform.position += new Vector3(1, 0, 0);
                if (!CheckIsValidPosition())
                {
                    tetromino.transform.position += new Vector3(-1, 0, 0);
                }
            }
            if (!CheckIsValidPosition())//проверяем столкновения с другими блками и фигурами 
            {
                tetromino.transform.position += new Vector3(-1, 0, 0);
                if (!CheckIsValidPosition())
                {
                    tetromino.transform.position += new Vector3(-1, 0, 0);
                }
            }
            if (CheckIsValidPosition())//проверяем столкновения с другими блками и фигурами 
            {
                game.UpdateGrid(this);//если все хорошо обновляем границу поля 
            }
            else //  иначе возвращаем фигуры в исходное положение
            {
                if (limitRotation)
                {
                    if (tetromino.transform.rotation.eulerAngles.z >= 90)
                    {
                        tetromino.transform.Rotate(0, 0, -90);
                    }
                    else
                    {
                        tetromino.transform.Rotate(0, 0, 90);
                    }
                }
                else
                {
                    transform.Rotate(0, 0, -90);
                }
            }
        }

    }
    public void CheckUserInput()//регестрация всех нажатий пользователя на клавиатуру для упраления фигурой 
    {

        movedImmediateHorizontal = false;
        movedImmediateVertical = false;

        horizontalTimer = 0;
        verticalTimer = 0;
        buttonDownWaitTimer = 0;

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
            if (game.GetTransformAtGridPosition(pos) != null && game.GetTransformAtGridPosition(pos).parent != transform)
            {
                return false;

            }
        }
        return true;
    }
}
