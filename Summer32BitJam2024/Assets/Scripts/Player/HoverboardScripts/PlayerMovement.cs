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
    public float maxSpeed;

    //Raycast
    public float distance;
    public Transform middle;

    public Vector3 mainVelocity;

    public Transform orientation;

    void Start()
    {
        //rb = GetComponent<Rigidbody>();

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

        //velocityX = Input.GetAxisRaw("Horizontal");
        velocityY = Input.GetAxisRaw("Vertical");

        mainVelocity = orientation.forward * velocityY + orientation.right * velocityX;

        velocity = Mathf.Clamp01(Mathf.Abs(velocityY)); //Add " + Mathf.Abs(velocityX)" to hadd horizontal movement 

        //rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);

        //rb.drag = playerDrag;
    }
    void FixedUpdate()
    {
        RaycastHit hit;

        if (Physics.Raycast(middle.position, middle.TransformDirection(-Vector3.up), out hit, distance))
            if(hit.collider.tag == "Floor")
                PlayerMove();
                
    }
    void PlayerMove()
    {
        rb.AddForce(mainVelocity.normalized * currentSpeed * velocity, ForceMode.Acceleration);
    }
}
