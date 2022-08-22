using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //доступ к классам навигации Unity

public class EnemyBehavior : MonoBehaviour
{
    public Transform Player; //переменную для хранения значения компонента Transform игрока
    public Transform PatroRoute; //переменную для хранения пустого родительского объекта PatrolRoute    
    public List<Transform> Locations; //переменная типа List для хранения всех дочерних компонентов Transform объекта PatrolRoute  
    private int locationIndex = 0; //переменную для хранения точки, к которой в данный момент идет противник    
    private NavMeshAgent agent; //переменную для хранения компонента NavMeshAgent, прикрепленного к объекту Enemy
    private int lives = 3; //переменная позволит нам управлять здоровьем врага
    public int EnemyLives
    {
        get { return lives; } //всегда возвращать lives
        private set //чтобы связать новое значение EnemyLives с lives
        { 
            lives = value;
            if (lives <= 0)
            {
                Destroy(this.gameObject);
                Debug.Log("Enemy destroied!");
            }
        }
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Player = GameObject.Find("MainPlayer").transform; //метод GameObject.Find("Player"), чтобы вернуть ссылку на объект игрока в сцене:
                                                          //добавление приписки.transform позволяет напрямую сослаться на компонент Transform в той же строке        
        InitializePatrolRoute();
        MoveToNextPatrolLocatoin();
    }

    private void Update()
    {
        if(agent.remainingDistance < 0.2f && !agent.pathPending) //свойство remainingDistance возвращает информацию о том, как далеко компонент NavMeshAgent находится от точки назначения
                                                                 //свойство pathPending возвращает истину или ложь в зависимсти от того, вычисляет ли Unity путь для компонента NavMesAgent
        {
            MoveToNextPatrolLocatoin();
        }
    }

    //приватный служебный метод для процедурного заполнения объекта locarions значениями Transform
    private void InitializePatrolRoute()
    {
        //для перебора каждого дочернего GameObject в объекте PatrolRoute и берем их компоненты Transform,
        //каждый компонент Transform записывается в локальной переменной child, объявленной в цикле foreach
        foreach (Transform child in PatroRoute) 
        {
            Locations.Add(child); //перебирая дочерние объекты в PatrolRoute, мы добавляем все childTransform в список Locations с помощью метода Add()            
        }
    }

    private void MoveToNextPatrolLocatoin()
    {
        if(Locations.Count == 0) //чтобы убедиться, что список Locations не пуст и можно спокойно выполнять метод
            return;
        agent.destination = Locations[locationIndex].position; //destination — это положение в трехмерном пространстве в формате Vector3

        //значению locationIndex мы прибавляем 1 и применяем остаток от деления на location.Count:y индекс будет увеличиваться с 0 до 4, а затем снова станет равен 0,
        //и траектория врага, таким образом, замкнется;
        //оператор модуля возвращает остаток от двух делимых значений — 2, деленное на 4, имеет остаток 2, поэтому 2 % 4 = 2.Аналогично 4, деленное на 4, не имеет остатка, поэтому 4 % 4 = 0
        locationIndex = (locationIndex + 1) % Locations.Count;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "MainPlayer")
        {
            agent.destination = Player.position; //устанавливаем значение agent.destination на положние игрока в всякий раз, когда игрок входит в зону атаки             
            Debug.Log("Player detected - attak!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "MainPlayer")
        {
            Debug.Log("Player out of range, resume patrol");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Bullet(Clone)")//c пулями сталкивается именно Enemy, разумно проверять эти столкновения  
        {
            EnemyLives -= 1;//если имя сталкивающегося объекта соответствует пуле, мы уменьшаем EnemyLives на 1 и выводим другое сообщение
            Debug.Log("Critical hit!");
        }
    }
}
