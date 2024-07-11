using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverboardDirection : MonoBehaviour
{
    public Rigidbody rb;

    public float currentSpeed;
    public float maxSpeed;

    public float velocityY;
    public float velocity;

    public Vector3 mainVelocity;


    void Update()
    {
        velocityY = Input.GetAxis("Horizontal");

        mainVelocity = transform.up * velocityY;

        //mainVelocity = new Vector3(0, mainVelocity.y, 0);

        velocity = Mathf.Clamp01(Mathf.Abs(velocityY));

        //rb.angularVelocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
    }
    void FixedUpdate()
    {
        //float h = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        PlayerRotateVelocity();
    }
    void PlayerRotate()
    {
        rb.AddTorque(mainVelocity.normalized * currentSpeed * velocity, ForceMode.Acceleration);
    }
    void PlayerRotateVelocity()
    {
        rb.angularVelocity = new Vector3(rb.angularVelocity.x, velocityY * currentSpeed, rb.angularVelocity.z);
    }
}
