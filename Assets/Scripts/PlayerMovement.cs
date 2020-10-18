using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    
    public float speed = 12f;
    public float gravity = -20f;
    public float jumpHeight = 2f;
    public float jumpBufferTime = 0.2f;
    public float groundDistance = 0.2f;
    public Transform groundCheck;
    public LayerMask groundMask;

    bool potatoed;
    Vector3 velocity;
    bool isGrounded;
    float currSpeed;
    float currJumpHeight;
    public float jumpBuffer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        currSpeed = speed;
        currJumpHeight = jumpHeight;
    }

    // Function called upon potatification
    void PotateOn(){
        potatoed = true;
        currSpeed = speed*1.5f;
        currJumpHeight = jumpHeight*1.5f;
        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        controller.Move(new Vector3(0, 2f, 0));
    }

    void PotateOff(){
        potatoed = false;
        currSpeed = speed;
        currJumpHeight = jumpHeight;
        transform.localScale = new Vector3(1f, 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0){
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right*x+transform.forward*z;
        controller.Move(move*currSpeed*Time.deltaTime);

        if (jumpBuffer > 0f){
            jumpBuffer -= Time.deltaTime;
        }
        if (Input.GetKeyDown("e")){
            jumpBuffer = jumpBufferTime;
        }

        if (jumpBuffer > 0f && isGrounded){
            velocity.y = currJumpHeight;
        }

        if (Input.GetKeyDown("f")){
            if (potatoed)
                PotateOff();
            else
                PotateOn();
        }

        velocity.y += gravity*Time.deltaTime;

        controller.Move(velocity*Time.deltaTime);
    }
}
