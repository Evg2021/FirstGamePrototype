using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroy : MonoBehaviour
{
    public float OnScreenDelay = 3f; //���������� ���� float, �������� �����, � ������� �������� ������� Bullet ������ ������������ �� ����� ����� ����, ��� ����� �������.
    private void Start()
    {
        Destroy(this.gameObject, OnScreenDelay); //������� GameObject �� � ������� ������ Destroy(). ������ Destroy() � �������� ��������� ������ ����� ������.
                                                 //� ������ ������ �� ���������� �������� ����� this, �������� �� ������, � �������� ���������� ��������.
                                                 //����� Destroy() ����� ��������� �������������� �������� ���� float � ����� ��������, ����� �������� ���� ����� ������������
    }

}
