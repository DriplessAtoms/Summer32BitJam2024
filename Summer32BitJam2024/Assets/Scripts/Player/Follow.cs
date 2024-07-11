using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform player;
    public Transform board;

    void Start()
    {
        player = GetComponent<Transform>();
    }
    void Update()
    {
        player.position = new Vector3(board.position.x, player.position.y, board.position.z);
    }
}
