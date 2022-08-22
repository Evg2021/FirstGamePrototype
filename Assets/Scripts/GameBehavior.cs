using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameBehavior : MonoBehaviour
{       
    public string LabelText = "Collect all 4 items and win your freedom!";
    public int MaxItems = 4;
    public bool ShowWinScreen = false; //��������� ���������� public bool, � ������� ��������  ���������� � ���, ����� � ���������� ������ ��������� ����� ������
    public bool ShowLoseScreen = false; //��������� ���������� public bool, � ������� �������� ���������� � ���, ����� � ���������� ������ ��������� ����� ���������
    public int Items
    {
        get { return itemCollected; }                       //� ������� get ������ ��������, ���������� � itemCollected, 
                                                            //����� ������� ����� �������� ���������� � Items
        set                                                 //� ������� set �� ����������� itemCollected ����� �������� Items ��� ������ ����������, ������� ���� ����� �����                                          
        {                                                   //������� Debug.LogFormat(), ����� ������� ���������� �������� itemCollected
            itemCollected = value;
            //Debug.LogFormat("Items: {0}", itemCollected);
            if(itemCollected >= MaxItems)
            {
                LabelText = "You've found all items!";
                ShowWinScreen = true;
                Time.timeScale = 0.0f;//�������� Time.timeScale �������� 0, ����� ���������������� ����, ����� ������������ ����� ������.����� ���� �� ������� ������������ ����� ��������������
            }
            else
            {
                LabelText = "Item found, only" + (MaxItems - itemCollected) + " more to go!";
            }
        }
    }   //�� �������� ����� ��������� ���������� Items �� ���������� ��� ������ � ��� ������    
    public int HP
    {
        get { return playerHP; }
        set { 
                playerHP = value;
            //Debug.LogFormat("Lives: {0}", playerHP);
            if (playerHP <= 0) // ����� ������� ������, ����� �������� playerHP ������ ���� 0
            {
                LabelText = "You want another live with that?";
                ShowLoseScreen = true;
                Time.timeScale = 0;
            }
            else 
            {
                LabelText = "Ouch... that's got hurt!";
            }
            }
    } //�� ������� ��������� ���������� � ������ HP � ���������� ��� ������ � ��� ������, ������� ����� ������������ � ���������� � ��������� ���������� playerHP
    
    private int itemCollected = 0;
    private int playerHP = 3;
    
    private void RestartLevel()
    {
        SceneManager.LoadScene(0);//����� LoadScene() ��������� ������ ����� ��� �������� int; � ��� ���� ������ ���� �����, �� ���������� ������ 0, ����� ������������� ���� � �����
        Time.timeScale = 1.0f;//�� ���������� �������� Time.timeScale �� �������� �� ��������� 1, ����� ��� ����������� ����� ��� �������� ���������� �����
    }
    private void OnGUI()
    {
        if (ShowWinScreen)
        {
            if(GUI.Button(new Rect(Screen.width/2 -100, Screen.height/2 - 50, 200,100), "YOU WON!"))
            {
                RestartLevel();
            }
        }
        if (ShowLoseScreen)
        {
            if(GUI.Button(new Rect(Screen.width/2 - 100, Screen.height/2 - 50, 200,100), "You lose...")) //����� ������������ �������� ������ ���������, ������� ���������������,
                                                                                                         //�������� timeScale ������������ �� 1 � ���������� ������������ � ������
            {
                RestartLevel();
            }
        }
        
        GUI.Box(new Rect(20, 20, 150, 25), "Player Health: " + playerHP);
        GUI.Box(new Rect(20,50,150, 25), "Items Collected|: " + itemCollected);
        GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height - 50, 300, 50), LabelText);
    }    
}
