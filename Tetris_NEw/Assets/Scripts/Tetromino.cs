using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetromino : MonoBehaviour
{
    private float fall = 0;//таймер обратного отсчета для скорости падения

    private float fallSpeed;// скорость падения 
    [SerializeField] private bool allowRotation = true;// переменная которая позволяет вращать фигуру 
    [SerializeField] private bool limitRotation = false;//переменная для ограничения вращения фигуры,для некторых фигур ограниченно вращение 90 и -90

    [SerializeField] private float continuousVerticalSpeed = 0.05f;//скорость фигуры если нажать и удерживать кнопку вниз
    [SerializeField] private  float continuousHorizontalSpeed = 0.1f;//скорость фигуры если нажать и удерживать кнопку влево или вправо
    [SerializeField] private float buttonDownWaitMax = 0.2f;// через какой промежуток времени игра зарегистрирует залипании клавиши 

    private  float verticalTimer = 0;//таймер движения вниз
    private  float horizontalTimer=0;//таймер движения по горизонтали
    private  float buttonDownWaitTimer = 0;//таймер нажатия клавиши

    private Game game;

    private  bool movedImmediateHorizontal = false;//движение по вертикали
    private bool movedImmediateVertical = false; //движения по горизонтали

    // Start is called before the first frame update
    void Start()
    {
         fallSpeed = GameObject.Find("GameScript").GetComponent<Game>().FallSpeed;//получем и присваиваем занчение переменноq fallSpeed из класса Game 

    }

    // Update is called once per frame
    void Update()
    {
        CheckUserInput();
    }

    public void Initialized(Game game)
    {
        this.game = game;
    }
    void CheckUserInput()//регестрация всех нажатий пользователя на клавиатуру для упраления фигурой 
    {
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.DownArrow)|| Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S))// если отпускает клавишу
        {
            movedImmediateHorizontal = false;
            movedImmediateVertical = false;

            horizontalTimer = 0;
            verticalTimer = 0;
            buttonDownWaitTimer = 0;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))//нажимаем клавишу D или стрелку Вправо
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
            transform.position += new Vector3(1, 0, 0);// двигаем вправо на 1 позицию 
            if (CheckIsValidPosition())
            {
                game.UpdateGrid(this);
            }
            else
            {
                transform.position += new Vector3(-1, 0, 0);// если занято ,возращаем прежнее положение 
            }
        }else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))//нажимаем клавишу A или стрелку Влево
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
            transform.position += new Vector3(-1, 0, 0);//двигаем влево на 1 позицию
            if (CheckIsValidPosition())
            {
                game.UpdateGrid(this);
            }
            else
            {
                transform.position += new Vector3(1, 0, 0);// если занято ,возращаем прежнее положение 
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space))// Нажимаем клавишу Space
        {
            if (allowRotation)//если мы можем вращать фигуру выполняем 
            {
                if (limitRotation)// если ограниченно вращение 
                {
                    if (transform.rotation.eulerAngles.z >= 90)
                    {
                        transform.Rotate(0, 0, -90);
                    }
                    else
                    {
                        transform.Rotate(0, 0, 90);
                    }
                }
                else// иначе вращаем
                {
                    transform.Rotate(0, 0, 90);
                }
                if (CheckIsValidPosition())//проверяем столкновения с другими блками и фигурами 
                {
                    game.UpdateGrid(this);//если все хорошо обновляем границу поля 
                }
                else//  иначе возвращаем фигуры в исходное положение
                {
                    if (limitRotation)
                    {
                        if (transform.rotation.eulerAngles.z >= 90)
                        {
                            transform.Rotate(0, 0, -90);
                        }
                        else
                        {
                            transform.Rotate(0, 0, 90);
                        }
                    }
                    else
                    {
                        transform.Rotate(0, 0, -90);
                    }
                }
            }
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S) || Time.time - fall >= fallSpeed)// если нажата кнопка S или стрелка Вниз или пришло время опустить фигуру вниз
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
            transform.position += new Vector3(0, -1, 0);//  двигаем вниз на 1 позицию
            if (CheckIsValidPosition())//проверка столкновений
            {
                game.UpdateGrid(this);// обновляем поле
            }
            else
            {
                transform.position += new Vector3(0, 1, 0);// отменяем перемещение
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
        }
    }
    bool CheckIsValidPosition()//  проверка столкновений 
    {
        foreach(Transform mino in transform)
        {
            Vector2 pos = game.Round(mino.position);
            if (game.CheckIsInsideGrid(pos) == false)
            {
                return false;
            }
            if (game.GetTransformAtGridPosition(pos)!=null && game.GetTransformAtGridPosition(pos).parent != transform)
            {
                return false;
            
            }
        }
        return true;
    }
}
