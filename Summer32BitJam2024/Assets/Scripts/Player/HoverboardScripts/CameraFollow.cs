using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform cam;
    public Transform orientation;

    void Start()
    {
        Application.targetFrameRate = 144;
        QualitySettings.vSyncCount = 0;
        cam = GetComponent<Transform>();
    }
    void Update()
    {
        cam.position = orientation.position;
        cam.rotation = Quaternion.Euler(0,orientation.rotation.eulerAngles.y,0);
    }
}
