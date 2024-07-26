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

    void FixedUpdate()
    {
        for (int i = 0; i < 4; i++)
        {
            Hover(anchors[i], hits[i], anchorForce[i]);
        }
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
}