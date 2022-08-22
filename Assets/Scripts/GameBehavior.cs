using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameBehavior : MonoBehaviour
{       
    public string LabelText = "Collect all 4 items and win your freedom!";
    public int MaxItems = 4;
    public bool ShowWinScreen = false; //объявляем переменную public bool, в которой хранится  информация о том, когда в интерфейсе должен появиться экран победы
    public bool ShowLoseScreen = false; //объявляем переменную public bool, в которой хранится информация о том, когда в интерфейсе должен появиться экран поражения
    public int Items
    {
        get { return itemCollected; }                       //С помощью get вернем значение, хранящееся в itemCollected, 
                                                            //когда внешний класс пытается обратиться к Items
        set                                                 //С помощью set мы присваиваем itemCollected новое значение Items при каждом обновлении, добавив сюда также вызов                                          
        {                                                   //функции Debug.LogFormat(), чтобы вывести измененное значение itemCollected
            itemCollected = value;
            //Debug.LogFormat("Items: {0}", itemCollected);
            if(itemCollected >= MaxItems)
            {
                LabelText = "You've found all items!";
                ShowWinScreen = true;
                Time.timeScale = 0.0f;//свойства Time.timeScale значение 0, чтобы приостанавливать игру, когда отображается экран победы.Любой ввод со стороны пользователя будет игнорироваться
            }
            else
            {
                LabelText = "Item found, only" + (MaxItems - itemCollected) + " more to go!";
            }
        }
    }   //Мы объявили новую публичную переменную Items со свойствами для чтения и для записи    
    public int HP
    {
        get { return playerHP; }
        set { 
                playerHP = value;
            //Debug.LogFormat("Lives: {0}", playerHP);
            if (playerHP <= 0) // чтобы поймать момент, когда значение playerHP падает ниже 0
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
    } //Мы создали публичную переменную с именем HP и свойствами для чтения и для записи, которую будем использовать в комбинации с приватной переменной playerHP
    
    private int itemCollected = 0;
    private int playerHP = 3;
    
    private void RestartLevel()
    {
        SceneManager.LoadScene(0);//метод LoadScene() принимает индекс сцены как параметр int; у нас есть только одна сцена, мы используем индекс 0, чтобы перезапустить игру с самог
        Time.timeScale = 1.0f;//Мы сбрасываем свойство Time.timeScale на значение по умолчанию 1, чтобы при перезапуске сцены все элементы управления ожили
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
            if(GUI.Button(new Rect(Screen.width/2 - 100, Screen.height/2 - 50, 200,100), "You lose...")) //когда пользователь нажимает кнопку поражения, уровень перезапускается,
                                                                                                         //значение timeScale сбрасывается на 1 и управление возвращается к игроку
            {
                RestartLevel();
            }
        }
        
        GUI.Box(new Rect(20, 20, 150, 25), "Player Health: " + playerHP);
        GUI.Box(new Rect(20,50,150, 25), "Items Collected|: " + itemCollected);
        GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height - 50, 300, 50), LabelText);
    }    
}
