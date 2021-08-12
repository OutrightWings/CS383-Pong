using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleMovement : MonoBehaviour
{
    public float speed = 10;
    protected Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    virtual protected void FixedUpdate()
    {
        float input = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector3(0,0, input * speed);
    }
}
