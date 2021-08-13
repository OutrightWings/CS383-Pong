using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pong : MonoBehaviour
{
    GameObject canvas;
    Rigidbody rb;
    public float baseSpeed;
    float speed;
    bool initialized;
    Vector3 startingPos;
    void Start()
    {
        canvas = GameObject.FindObjectOfType<Canvas>().gameObject;
        rb = GetComponent<Rigidbody>();
        initialized = false;
        startingPos = transform.position;
        speed = baseSpeed;
    }
    void Update() {
        if (Input.GetKey("escape"))
            Application.Quit();
        if (Input.GetKey("r"))
            OnTriggerEnter(null);
        if (!initialized) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                //set some new random angle for it to start at
                float angle;
                do {
                    angle = Random.Range(-45, 45);
                    transform.Rotate(0, angle, 0, Space.World);
                } while (angle == 0);
                initialized = true;
                canvas.transform.Find("Help").gameObject.SetActive(false);
            }
        }
    }
    void FixedUpdate()
    {
        if(initialized) {
            Vector3 forward = transform.forward;
            forward *= speed;
            rb.velocity = forward;
        }
    }

    private void OnCollisionEnter(Collision collision) {
        string tag = collision.transform.gameObject.tag;
        Vector3 direction = transform.forward;

        if (tag == "Paddle") {
            //Level ups?
            speed += .5f;
            //hit the paddle math to make it change based on where it was hit
            Vector3 contact = collision.contacts[0].point;                                                  //get the point it hit
            float ang = Mathf.Abs(90 - Vector3.Angle(contact,collision.transform.position)) * 5;            //get the angle from the ball's hit to the paddle
            direction = transform.rotation.eulerAngles;                                                     //get rotation of ball
            direction.y = direction.y > 180 ?  270 + ang :  90 - ang;                                       //change how we add the angle it hit with based on how far its currently rotated so if it came in going to the right, it still goes right
            direction.y += collision.gameObject.name == "Right" ? -90 : 90;                                 //I originally wrote this for a paddle at the bottom of the screen, so this is to rotate it +/-90 degrees depedning if on the left or right side of the screen
            transform.rotation = Quaternion.Euler(direction);                                               //Math.png gives a visual diagram of how this works
        }
        else {
            if (name == "Side") {
                //hit a side wall
                //flip x
                direction.x *= -1;
            }
            else {
                //hit a top wall
                //flip z
                direction.z *= -1;
            }


            Quaternion rot = Quaternion.FromToRotation(transform.forward, direction);
            transform.Rotate(rot.eulerAngles);
        }
        
    }
    private void OnTriggerEnter(Collider other) {
        //hit a wall that "destroys" the ball
        canvas.transform.Find("Help").gameObject.SetActive(true);
        initialized = false;
        transform.rotation = Quaternion.identity;
        transform.position = startingPos;
        rb.velocity = Vector3.zero;
        speed = baseSpeed;
    }
}
