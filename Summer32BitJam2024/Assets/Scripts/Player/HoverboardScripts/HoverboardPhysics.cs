using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverboardPhysics : MonoBehaviour
{
    public Transform[] anchors = new Transform[4];
    public RaycastHit[] hits = new RaycastHit[4];
    public float[] anchorForce = new float[4];

    public Rigidbody rb;
    public float maxForce;
    public Transform Middle;
    public float distance;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            /*anchorForce[0] = 0;
            anchorForce[1] = 0;
            anchorForce[2] = 0;
            anchorForce[3] = 0;*/
            maxForce = 0;
        }
        else if (Input.GetKeyUp("q"))
        {/*
            anchorForce[0] = 1;
            anchorForce[1] = 1;
            anchorForce[2] = 1;
            anchorForce[3] = 1;*/
            maxForce = 3;
        }
        if(Input.GetKeyDown("r"))
        {
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            rb.angularVelocity = new Vector3(0, 0, 0);
        }
        if(Input.GetKeyDown("f"))
        {
            rb.velocity = new Vector3(0, 0, 0);
        }

        //Lock Rotation of Hoverboard
        //float lockedRot = Mathf.Clamp(transform.rotation.eulerAngles.x, -60, 60);
        //transform.rotation = Quaternion.Euler(lockedRot, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        //Lock Rotation and Stop Vertical Rotation Velocity
    }

    void FixedUpdate()
    {
        for (int i = 0; i < 4; i++)
        {
            Hover(anchors[i], hits[i], anchorForce[i]);
        }
        Slope();
    }
    
    private void Hover(Transform anchor, RaycastHit hit, float mult)
    {
        if (Physics.Raycast(anchor.position, -anchor.up, out hit))
        {
            float force = Mathf.Abs(1 / (hit.point.y - anchor.position.y));
            float clampedForce = Mathf.Clamp(force * mult, 0, maxForce);
            rb.AddForceAtPosition(transform.up * clampedForce, anchor.position, ForceMode.Acceleration);
        }
    }
    private void Slope()
    {
        RaycastHit hit;

        if (Physics.Raycast(Middle.position, Middle.TransformDirection(-Vector3.up), out hit, distance))
        {
            //Debug.Log(hit.collider.name);
            //maxForce = 100;
            /*if (hit.collider.tag == "Ramp")
            {
                //maxForce = 100;
                anchorForce[0] = 3;
                anchorForce[1] = 3;
                anchorForce[2] = 1;
                anchorForce[3] = 1;
            }*/
        }
        else
        {
            //maxForce = 10;
            /*anchorForce[0] = 1;
            anchorForce[1] = 1;
            anchorForce[2] = 1;
            anchorForce[3] = 1;*/
        }
    }
}
