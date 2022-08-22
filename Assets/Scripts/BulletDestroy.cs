using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroy : MonoBehaviour
{
    public float OnScreenDelay = 3f; //переменную типа float, хранящую время, в течение которого префабы Bullet должны существовать на сцене после того, как будут созданы.
    private void Start()
    {
        Destroy(this.gameObject, OnScreenDelay); //Удаляем GameObject мы с помощью метода Destroy(). Методу Destroy() в качестве параметра всегда нужен объект.
                                                 //В данном случае мы используем ключевое слово this, ссылаясь на объект, к которому прикреплен сценарий.
                                                 //Метод Destroy() может принимать дополнительный параметр типа float — время задержки, после которого пуля будет уничтожаться
    }

}
