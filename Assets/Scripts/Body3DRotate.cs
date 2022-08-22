using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body3DRotate : MonoBehaviour
{
    public Transform HeadPlayer;
    public float SensitivityMouse = 200f;            
    private float clampRotation = 0.0f;
    private float watchingAround;

    private void Update()
    {
        watchingAround = SensitivityMouse * Time.deltaTime;
        float mouseX = Input.GetAxis("Mouse X") * watchingAround; 
        float mouseY = Input.GetAxis("Mouse Y") * watchingAround; 

        clampRotation -= mouseY;
        clampRotation = Mathf.Clamp(clampRotation, -90.0f, 90.0f);

        transform.localRotation = Quaternion.Euler(clampRotation, 0.0f, 0.0f);
        HeadPlayer.Rotate(Vector3.up * mouseX);       
    }
}
