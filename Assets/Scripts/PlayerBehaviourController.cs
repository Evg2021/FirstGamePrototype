using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviourController : MonoBehaviour
{
    
    public Text TextCoins; //���������� ��� �������� ������ 
    public GameObject Bullet;//���������� ��� �������� ������� ����
    public Image[] InImages;//������ ��������
    public Sprite[] Spr;//������ ��������

    private float moveSpeed = 10.0f; //�������� �������� ������    
    private Vector3 moveForward; //���������� ��� �������� ������� ����������� ������
    private Vector3 moveRight; //���������� ��� �������� ������� ����������� � ���
    private float fInput; //���������� ��� �������� ��������� ����������� ������ WS
    private float rInput; //���������� ��� �������� ��������� ����������� � ��� AD
    private float bulletSpeed = 50.0f;//�������� �������� ����
    private float jumpVelocity = 5.0f; //���������� � ������� ����� ������� �������� �������� ���� ������
    private Rigidbody rb;
    private GameBehavior gameManager; //���������� ��� �������� ������ �� ��������� GameBehavior �� �����
    private int coins; //���������� ��� �������� ���������� ������� (���������)
    private bool ground;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        gameManager = GameObject.Find("GameManager").GetComponent<GameBehavior>(); //������� � ���������� �������� GameBehavior, ������������� � ������� Game Manager �� �����
                                                                                   //������������� ������ GetComponent() � ��� �� ������, ��� � GameObject.Find(), ��������� ���������� �� ������� ��
    }
    private void Update()
    {
        MovePlayer();
        Jump();
        BulletBehaviour();
    }   
    
    private void MovePlayer()
    {
        fInput = Input.GetAxis("Vertical") * moveSpeed; // ���������� ��� �������� ������ � ����������� ������
        rInput = Input.GetAxis("Horizontal") * moveSpeed; // ���������� ��� �������� ������ � ����������� � ���        

        moveForward = transform.forward * fInput; 
        transform.position += moveForward * Time.deltaTime;

        moveRight = transform.right * rInput;
        transform.position += moveRight * Time.deltaTime;   
    }

    private void BulletBehaviour()
    {
        if (Bullet != null && Input.GetMouseButtonDown(0))
        {
            //������� ��������� ���������� GameObject ������ ���, �����  �������� ���: ���������� ����� Instantiate(), ����� ��������� �������� GameObject �������� newBullet,
            //������� ������ Bullet. ���������� ��������� �������, ����� ���� ��������� ����� �������, ��� ����� ������� ������������;
            //� ����� ��������� ������� as GameObject, ����� ���� �������� ������������ ������ � ���� �� ����, ��� � newBullet
            GameObject newBullet = Instantiate(Bullet, this.transform.position + new Vector3(0, 0, 1), this.transform.rotation) as GameObject;

            Rigidbody bulletRB = newBullet.GetComponent<Rigidbody>();// ����� GetComponent(), ����� ������� � ��������� ��������� Rigidbody ������� newBullet

            bulletRB.velocity = this.transform.forward * bulletSpeed;//������������� �������� velocity ���������� Rigidbody � ���������� transform.forward ������ � �������� ���
                                                                     //�� bulletSpeed: ��������� ��������� velocity ������ ������������� AddForce() �����������,
                                                                     //��� ���������� �� ����� ������ ���� � ����� �� ���� ��� ��������
        }        
    }

    private void Jump()
    {
        if (ground && Input.GetKeyDown(KeyCode.Space)) //��������� � ��� ��� ���� ��������� Rigidbody, �� ����� �������� ��� ��������� Vector3 � ForceMode ��� ������ RigidBody.AddForce(), 
                                                       //��� ����� �������� ������ �������. y �� ���������, ��� ������(��� ����������� ����) ������
                                                       //���� ��������� �����, � ��� �������� ���������� �� �������� jumpVelocity.
                                                       //y �������� ForceMode ����������, ��� ����������� ����, � ����� �������� ����� ������������.
                                                       //�������� Impulse �������� ������� ���������� ���� � ������ ��� �����, ��� �������� �������� ��� ���������� �������� ������
        {
            rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Money")
        {
            coins++;  //����������� ���������� ����� �� ����
            other.gameObject.SetActive(false); //��������� �������
            TextCoins.text = coins.ToString(); // ��������� ��������� ��������� ����� � ������
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

    private void OnCollisionEnter(Collision collision) //��������� ������ � �������� Player ����� ������������ ����, ����� ����� �������� ����� OnCollisionEnter()
    {
        if(collision.gameObject.name == "Enemy") //��������� ��� ��������������� �������; ���� ��� ������ Enemy, �� ��������� �������� if.
        {
            gameManager.HP -= 1; //�������� 1 �� �������� ������ � ������� ���������� gameManager
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
