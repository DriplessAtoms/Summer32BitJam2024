using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverboardMovement : MonoBehaviour
{
    public Rigidbody rb;

    public float maxSpeed;
    public float currentSpeed;
    private float velocityY;
    private float velocity;
    
    public Transform orientation;
    public Vector3 mainVelocity;

    public Transform middle;
    public float distance;

    public float rotateVelocity;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        velocityY = Input.GetAxisRaw("Vertical");

        mainVelocity = orientation.forward * velocityY;

        velocity = Mathf.Clamp01(Mathf.Abs(velocityY));

        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
    }
    void FixedUpdate()
    {
        RaycastHit hit;

        if (Physics.Raycast(middle.position, middle.TransformDirection(-Vector3.up), out hit, distance))
        {
            if (hit.collider.tag == "Floor")
                PlayerMoveVelocity();
            else if(hit.collider.tag == "Ramp")
            {
                PlayerMoveVelocity();
                ChangeRotation(hit.collider.transform.rotation);
            }
        }
        else
            DisableRotation();
    }
    void PlayerMove()
    {
        rb.AddForce(mainVelocity.normalized * currentSpeed * velocity, ForceMode.Acceleration);
    }
    void PlayerMoveVelocity()
    {
        rb.drag = 1.25f;
        if (rb.velocity.magnitude < maxSpeed)
            rb.AddForce(mainVelocity * currentSpeed);
    }
    void PlayerRotateY()
    {
        rb.angularVelocity = new Vector3(velocityY * rotateVelocity, rb.angularVelocity.y, rb.angularVelocity.z);
    }
    void DisableRotation()
    {
        rb.angularVelocity = new Vector3(0, 0, 0);
        rb.rotation = Quaternion.Euler(0, rb.rotation.eulerAngles.y, 0);
        rb.drag = 0;
    }
    void ChangeRotation(Quaternion rampRot)
    {
        rb.rotation = Quaternion.Euler(rampRot.eulerAngles.x, rb.rotation.eulerAngles.y, rampRot.eulerAngles.z);
    }
}
