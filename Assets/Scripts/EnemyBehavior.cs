using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //������ � ������� ��������� Unity

public class EnemyBehavior : MonoBehaviour
{
    public Transform Player; //���������� ��� �������� �������� ���������� Transform ������
    public Transform PatroRoute; //���������� ��� �������� ������� ������������� ������� PatrolRoute    
    public List<Transform> Locations; //���������� ���� List ��� �������� ���� �������� ����������� Transform ������� PatrolRoute  
    private int locationIndex = 0; //���������� ��� �������� �����, � ������� � ������ ������ ���� ���������    
    private NavMeshAgent agent; //���������� ��� �������� ���������� NavMeshAgent, �������������� � ������� Enemy
    private int lives = 3; //���������� �������� ��� ��������� ��������� �����
    public int EnemyLives
    {
        get { return lives; } //������ ���������� lives
        private set //����� ������� ����� �������� EnemyLives � lives
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
        Player = GameObject.Find("MainPlayer").transform; //����� GameObject.Find("Player"), ����� ������� ������ �� ������ ������ � �����:
                                                          //���������� ��������.transform ��������� �������� ��������� �� ��������� Transform � ��� �� ������        
        InitializePatrolRoute();
        MoveToNextPatrolLocatoin();
    }

    private void Update()
    {
        if(agent.remainingDistance < 0.2f && !agent.pathPending) //�������� remainingDistance ���������� ���������� � ���, ��� ������ ��������� NavMeshAgent ��������� �� ����� ����������
                                                                 //�������� pathPending ���������� ������ ��� ���� � ���������� �� ����, ��������� �� Unity ���� ��� ���������� NavMesAgent
        {
            MoveToNextPatrolLocatoin();
        }
    }

    //��������� ��������� ����� ��� ������������ ���������� ������� locarions ���������� Transform
    private void InitializePatrolRoute()
    {
        //��� �������� ������� ��������� GameObject � ������� PatrolRoute � ����� �� ���������� Transform,
        //������ ��������� Transform ������������ � ��������� ���������� child, ����������� � ����� foreach
        foreach (Transform child in PatroRoute) 
        {
            Locations.Add(child); //��������� �������� ������� � PatrolRoute, �� ��������� ��� childTransform � ������ Locations � ������� ������ Add()            
        }
    }

    private void MoveToNextPatrolLocatoin()
    {
        if(Locations.Count == 0) //����� ���������, ��� ������ Locations �� ���� � ����� �������� ��������� �����
            return;
        agent.destination = Locations[locationIndex].position; //destination � ��� ��������� � ���������� ������������ � ������� Vector3

        //�������� locationIndex �� ���������� 1 � ��������� ������� �� ������� �� location.Count:y ������ ����� ������������� � 0 �� 4, � ����� ����� ������ ����� 0,
        //� ���������� �����, ����� �������, ���������;
        //�������� ������ ���������� ������� �� ���� ������� �������� � 2, �������� �� 4, ����� ������� 2, ������� 2 % 4 = 2.���������� 4, �������� �� 4, �� ����� �������, ������� 4 % 4 = 0
        locationIndex = (locationIndex + 1) % Locations.Count;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "MainPlayer")
        {
            agent.destination = Player.position; //������������� �������� agent.destination �� �������� ������ � ������ ���, ����� ����� ������ � ���� �����             
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
        if (collision.gameObject.name == "Bullet(Clone)")//c ������ ������������ ������ Enemy, ������� ��������� ��� ������������  
        {
            EnemyLives -= 1;//���� ��� ��������������� ������� ������������� ����, �� ��������� EnemyLives �� 1 � ������� ������ ���������
            Debug.Log("Critical hit!");
        }
    }
}
