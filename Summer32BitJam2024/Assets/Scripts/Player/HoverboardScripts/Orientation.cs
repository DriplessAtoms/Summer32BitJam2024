using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orientation : MonoBehaviour
{
    public void reflectOrientation(Vector3 rampRot)
    {
        transform.up = new Vector3(rampRot.x, rampRot.y, rampRot.z);
    }
}
