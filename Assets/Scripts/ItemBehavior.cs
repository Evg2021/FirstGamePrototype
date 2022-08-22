using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior : MonoBehaviour
{
    public GameBehavior GameManager; //�� ������� ����� ���������� ���� GameBehavior � ������ � ��� ������ �� ������������� ��������    

    private void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameBehavior>(); //�������������� gameManager, �������� ����� ������� Find(), � ����� �������� ������� GetComponent()        
    }
    
    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "MainPlayer")
        {
            Destroy(this.transform.parent.gameObject);
            Debug.Log("Bonus collected");
            GameManager.Items += 1; //����������� �������� Items � ������ gameManager � ������ OnCollisionEnter() ����� ����������� ������� Item             
        }
    }
}
