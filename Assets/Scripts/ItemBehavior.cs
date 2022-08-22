using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior : MonoBehaviour
{
    public GameBehavior GameManager; //Мы создаем новую переменную типа GameBehavior и храним в ней ссылку на прикрепленный сценарий    

    private void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameBehavior>(); //инициализируем gameManager, осмотрев сцену методом Find(), а затем вызываем функцию GetComponent()        
    }
    
    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "MainPlayer")
        {
            Destroy(this.transform.parent.gameObject);
            Debug.Log("Bonus collected");
            GameManager.Items += 1; //увеличиваем свойство Items в классе gameManager в методе OnCollisionEnter() после уничтожения префаба Item             
        }
    }
}
