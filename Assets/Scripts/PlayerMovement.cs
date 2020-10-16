using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    
    public float speed = 12f;
    public float gravity = -20f;
    public float jumpHeight = 2f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0){
            velocity.y = -10f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right*x+transform.forward*z;
        controller.Move(move*speed*Time.deltaTime);

        if (Input.GetAxis("Jump") != 0 && isGrounded){
            velocity.y = jumpHeight;
        }

        velocity.y += gravity*Time.deltaTime;

        controller.Move(velocity*Time.deltaTime);
    }
}
