using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float sensX, sensY;

    public Transform cam;
    public Transform orientation;

    public float mouseX, mouseY;
    public float multiplier = 0.01f;

    public float xRotation, yRotation;

    void Start()
    {
        //Application.targetFrameRate = 10;
        //QualitySettings.vSyncCount = 0;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        mouseX = Input.GetAxisRaw("Mouse X") * sensX;
        mouseY = Input.GetAxisRaw("Mouse Y") * sensY;

        xRotation -= mouseY;
        yRotation += mouseX;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cam.transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
