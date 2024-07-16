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

    public bool onRampForward;

    public Transform front, back;

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
                ChangeRotation(hit.normal);
                //Test(hit.normal);
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
    void ChangeRotation(Vector3 rampRot)
    {
        float xNormal = Vector3.Angle(rampRot, Vector3.up);

        //Different Method Begin

        //float normal_FTR = Mathf.Round((Quaternion.FromToRotation(Vector3.up, rampRot).eulerAngles.x-180) / Mathf.Abs((Quaternion.FromToRotation(Vector3.up, rampRot).eulerAngles.x-180)));//HAS NOTHING TO DO WITH DIRECTION

        //xNormal *= -normal_FTR; //HAS NOTHING TO DO WITH DIRECTION
        //xNormal *= -Mathf.Round(transform.forward.x);

        xNormal = -Mathf.Rad2Deg * Mathf.Atan((rampRot.z + rampRot.x) / rampRot.y); //HAS EVERYTHING TO DO WITH DIRECTION >:D
        //Different Method End
        float xAngle = 0;
        int betterExample = (int)(rampRot.z*10);
        if (betterExample > 0 || betterExample < 0)
            xAngle = (xNormal / 90) * Mathf.Abs(transform.rotation.eulerAngles.y - 180) - xNormal;
        else if (transform.rotation.eulerAngles.y < 270 && (rampRot.x > 0 || rampRot.x < 0))
            xAngle = (((-xNormal) / 90) * Mathf.Abs(transform.rotation.eulerAngles.y - 90) + xNormal); //changes to formula here!!!
        else if (transform.rotation.eulerAngles.y >= 270)
            xAngle = (((xNormal) / 90) * Mathf.Abs(transform.rotation.eulerAngles.y - 270) - xNormal);
        float zNormal = Vector3.Angle(rampRot, Vector3.right) - 90;

        float zAngle = ((zNormal / 90) * Mathf.Abs(transform.rotation.eulerAngles.y - 180) - zNormal);

        rb.rotation = Quaternion.Euler(-xAngle, transform.rotation.eulerAngles.y, 0);
        //(Vector3.Angle(rampRot, Vector3.right) - 90)

        //Debug.Log(Mathf.Rad2Deg *Mathf.Atan((rampRot.z+rampRot.x)/ rampRot.y) + " " + rampRot);
        //Debug.Log(xAngle);
    }
    void Test(Vector3 rampRot)
    {
        //Quaternion hoverboardRot = Quaternion.FromToRotation(Vector3.up, rampRot);

        // transform.rotation = Quaternion.Euler(hoverboardRot.eulerAngles.x, transform.rotation.eulerAngles.y, hoverboardRot.eulerAngles.z);

        //transform.rotation = hoverboardRot * (Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0));
    }
}
