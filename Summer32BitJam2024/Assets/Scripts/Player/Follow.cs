using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform follower;
    public Transform followed;

    void Start()
    {
        follower = GetComponent<Transform>();
    }
    void Update()
    {
        follower.position = new Vector3(followed.position.x, followed.position.y-0.2f, followed.position.z);
    }
}
