using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;

    public float currentSpeed;
    public float walkSpeed = 50;
    public float sprintSpeed = 100;
    public float playerDrag;
    public float velocityX, velocityY;
    public float velocity;

    public Vector3 mainVelocity;

    public Transform orientation;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        currentSpeed = walkSpeed;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = sprintSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            currentSpeed = walkSpeed;
        }

        velocityX = Input.GetAxisRaw("Horizontal");
        velocityY = Input.GetAxisRaw("Vertical");

        mainVelocity = orientation.forward * velocityY + orientation.right * velocityX;

        velocity = Mathf.Clamp01(Mathf.Abs(velocityY) + Mathf.Abs(velocityX));

        rb.drag = playerDrag;
    }
    void FixedUpdate()
    {
        PlayerMove();
    }
    void PlayerMove()
    {
        rb.AddForce(mainVelocity.normalized * currentSpeed * velocity, ForceMode.Acceleration);
    }
}
