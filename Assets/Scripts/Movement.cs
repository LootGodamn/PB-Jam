using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
	private float speed = 1.0f;
	public float sensitivity = 30.0f;
	CharacterController character;
	Rigidbody rb;
	public GameObject cam;
	float moveFB, moveLR;
	float rotX, rotY;
	float gravity = -10.8f;
	private bool Locked = false;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		character = GetComponent<CharacterController>();
	}

	public void LockCursor()
	{
		if (Input.GetKeyDown(KeyCode.LeftControl) && Locked == false)
		{
			Cursor.lockState = CursorLockMode.Locked;
			Locked = true;
			Cursor.visible = false;
		}
		else if (Input.GetKeyDown(KeyCode.LeftControl) && Locked == true)
		{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			Locked = false;
		}
	}

	void Update()
	{
		LockCursor();
		Jump();
		Run();

		moveFB = Input.GetAxis("Horizontal") * speed;
		moveLR = Input.GetAxis("Vertical") * speed;


		rotX = Input.GetAxis("Mouse X") * sensitivity;
		rotY = Input.GetAxis("Mouse Y") * sensitivity;


		Vector3 movement = new Vector3(moveFB, gravity * Time.deltaTime, moveLR);


		

		CameraRotation(cam, rotX, rotY);

		movement = transform.rotation * movement;
		character.Move(movement * Time.deltaTime);
		bool isGrounded = character.isGrounded;
	}


	void CameraRotation(GameObject cam, float rotX, float rotY)
	{
		
		transform.Rotate(0, rotX * Time.deltaTime, 0);
		cam.transform.Rotate(-rotY * Time.deltaTime, 0, 0);
	}

	public void Jump()
	{
		Vector3 movingDir = Vector3.zero;
		bool isGrounded = character.isGrounded;
		if (Input.GetAxis("Jump") > 0 && isGrounded)
		{
			transform.position += Vector3.up * gravity;
		}
	}

	public void Run()
	{
		if (Input.GetAxis("Run") > 0)
		{
			speed = 6.0f;
		} else
		{
			speed = 3.0f;
		}
	}
}
