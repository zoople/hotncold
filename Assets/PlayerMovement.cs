using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;            // The speed that the player will move at.

    Vector3 movement;                   // The vector to store the direction of the player's movement.
    Animator anim;                      // Reference to the animator component.
    Rigidbody playerRigidbody;          // Reference to the player's rigidbody.

    bool jumping = false;
    // Use this for initialization
    void Start()
    {
        //anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        
    }


    void FixedUpdate()
    {
        // Store the input axes.
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        
        if (Mathf.Abs(h) > 0.001f || Mathf.Abs(v) > 0.001f)
        {
            //Debug.Log("Move: " + h + "," + v);
            Move(h, v);
        }
        else GetComponent<Animator>().SetFloat("Speed", 0.0f);

        float z = Input.GetAxis("Fire1");
        if (z > 0 && jumping == false)
        {
            Debug.Log(z);
            jumping = true;
            playerRigidbody.AddForce(new Vector3(0.0f, 2550.0f, 0));
            //Debug.Log("Jump");
            Debug.Log("FIRE:" + z);

        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Cat hit the ground");
        jumping = false;
    }


    void Move(float h, float v)
    {

        GetComponent<Animator>().SetBool("isMoving", true);
        GetComponent<Animator>().SetFloat("Speed", 1.0f);

        // Set the movement vector based on the axis input.
        movement.Set(h, 0f, v);

        // Normalise the movement vector and make it proportional to the speed per second.
        movement = movement * speed * Time.deltaTime;

        Debug.Log(movement.magnitude / speed / Time.deltaTime);
        GetComponent<Animator>().SetFloat("Speed", movement.magnitude / speed / Time.deltaTime);

        // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
        Quaternion newRotation = Quaternion.LookRotation(movement);

        // Set the player's rotation to this new rotation.
        playerRigidbody.MoveRotation(newRotation);

        // Move the player to it's current position plus the movement.
        playerRigidbody.MovePosition(transform.position + movement);

        //gameObject.GetComponent<Animator>().SetFloat("Speed", movement.);

    }


}
