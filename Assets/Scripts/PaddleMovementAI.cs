using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleMovementAI : PaddleMovement
{
    public Transform pong;
    protected override void FixedUpdate() {
        float direction = Mathf.Clamp(-(transform.position.z - pong.position.z), -1, 1);
        rb.velocity = new Vector3(0, 0, direction * speed);
    }
}
