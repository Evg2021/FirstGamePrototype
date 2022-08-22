using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviourController : MonoBehaviour
{
    
    public Text TextCoins; //переменная для хранения текста 
    public GameObject Bullet;//переменная для хранения префаба пули
    public Image[] InImages;//массив картинок
    public Sprite[] Spr;//массив спрайтов

    private float moveSpeed = 10.0f; //скорость движения вперед    
    private Vector3 moveForward; //переменная для хранения вектора перемещения вперед
    private Vector3 moveRight; //переменная для хранения вектора перемещения в бок
    private float fInput; //переменная для хранения координат перемещения вперед WS
    private float rInput; //переменная для хранения координат перемещения в бок AD
    private float bulletSpeed = 50.0f;//скорость движения пули
    private float jumpVelocity = 5.0f; //переменная в которой будем хранить желаемую величину силы прыжка
    private Rigidbody rb;
    private GameBehavior gameManager; //переменную для хранения ссылки на экземпляр GameBehavior на сцене
    private int coins; //переменная для хранения количества монеток (собранных)
    private bool ground;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        gameManager = GameObject.Find("GameManager").GetComponent<GameBehavior>(); //находим и возвращаем сценарий GameBehavior, прикрепленный к объекту Game Manager на сцене
                                                                                   //использование метода GetComponent() в той же строке, что и GameObject.Find(), позволяет избавиться от лишнего ко
    }
    private void Update()
    {
        MovePlayer();
        Jump();
        BulletBehaviour();
    }   
    
    private void MovePlayer()
    {
        fInput = Input.GetAxis("Vertical") * moveSpeed; // переменная для хранения данных о перемещении вперед
        rInput = Input.GetAxis("Horizontal") * moveSpeed; // переменная для хранения данных о перемещении в бок        

        moveForward = transform.forward * fInput; 
        transform.position += moveForward * Time.deltaTime;

        moveRight = transform.right * rInput;
        transform.position += moveRight * Time.deltaTime;   
    }

    private void BulletBehaviour()
    {
        if (Bullet != null && Input.GetMouseButtonDown(0))
        {
            //создаем локальную переменную GameObject каждый раз, когда  нажимаем лкм: используем метод Instantiate(), чтобы назначить свойству GameObject значение newBullet,
            //передав префаб Bullet. Используем положение капсулы, чтобы пуля появилась перед игроком, тем самым избегая столкновений;
            //в конце добавляем строчку as GameObject, чтобы явно привести возвращаемый объект к тому же типу, что и newBullet
            GameObject newBullet = Instantiate(Bullet, this.transform.position + new Vector3(0, 0, 1), this.transform.rotation) as GameObject;

            Rigidbody bulletRB = newBullet.GetComponent<Rigidbody>();// метод GetComponent(), чтобы вернуть и сохранить компонент Rigidbody объекта newBullet

            bulletRB.velocity = this.transform.forward * bulletSpeed;//Устанавливаем свойство velocity компонента Rigidbody в направлнии transform.forward игрока и умножаем его
                                                                     //на bulletSpeed: изменение параметра velocity вместо использования AddForce() гарантирует,
                                                                     //что гравитация не будет тянуть пули к земле по дуге при выстреле
        }        
    }

    private void Jump()
    {
        if (ground && Input.GetKeyDown(KeyCode.Space)) //Поскольку у нас уже есть компонент Rigidbody, мы можем передать ему параметры Vector3 и ForceMode для метода RigidBody.AddForce(), 
                                                       //тем самым заставив игрока прыгать. y Мы указываем, что вектор(или приложенная сила) должен
                                                       //быть направлен вверх, и его величина умножается на параметр jumpVelocity.
                                                       //y Параметр ForceMode определяет, как применяется сила, и также является типом перечисления.
                                                       //Параметр Impulse передает объекту мгновенную силу с учетом его массы, что идеально подходит для реализации механики прыжка
        {
            rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Money")
        {
            coins++;  //увеличиваем количество монет на одну
            other.gameObject.SetActive(false); //отключаем монетку
            TextCoins.text = coins.ToString(); // переводим количесво собранных монет в строку
        }
        if (other.tag == "arrow")
        {
            InImages[0].sprite = Spr[0];
        }
        if (other.tag == "gold")
        {
            InImages[1].sprite = Spr[1];
        }
        if (other.tag == "sword")
        {
            InImages[2].sprite = Spr[2];
        }
    }

    private void OnCollisionEnter(Collision collision) //Поскольку именно с объектом Player будет сталкиваться враг, имеет смысл объявить метод OnCollisionEnter()
    {
        if(collision.gameObject.name == "Enemy") //проверяем имя сталкивающегося объекта; если это префаб Enemy, то выполняем оператор if.
        {
            gameManager.HP -= 1; //вычитаем 1 из здоровья игрока с помощью экземпляра gameManager
        }
        if (collision.gameObject.tag == "Ground")
        {
            ground = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            ground = false;
        }
    }
}
