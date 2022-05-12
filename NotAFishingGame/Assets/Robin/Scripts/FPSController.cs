using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    public Transform playerCamera;
    public Rigidbody rb;

    public float speed;
    public float walkingSpeed;
    public float runningSpeed;
    public float maxSpeed;
    public float airMulti;

    public bool running;

    public bool grounded;

    public float drag;

    public bool readyJump;
    public bool jumped;
    public float jumpForce;

    float xRotation;
    float yRotation;
    public float mouseSens;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        PlayerInput();
        Camera();
        Run();
        SpeedControl();
    }

    void FixedUpdate()
    {
        Movement();
        Jump();
    }

    void PlayerInput()
    {
        if(Input.GetButton("Shift") && grounded)
        {
            running = true;
        }
        else if(Input.GetButtonUp("Shift"))
        {
            running = false;
        }

        if(Input.GetButtonDown("Jump"))
        {
            readyJump = true;
        }
    }

    void Movement()
    {
        float moveZ = Input.GetAxisRaw("Vertical");
        float moveX = Input.GetAxisRaw("Horizontal");

        if(grounded)
        {
            rb.AddForce(transform.forward.normalized * moveZ * speed * 10f, ForceMode.Force);
            rb.AddForce(transform.right.normalized * moveX * speed * 10f, ForceMode.Force);
        }
        else if(!grounded)
        {
            rb.AddForce(transform.forward.normalized * moveZ * speed * 10f * airMulti, ForceMode.Force);
            rb.AddForce(transform.right.normalized * moveX * speed * 10f * airMulti, ForceMode.Force);
        }

        if(grounded)
        {
            rb.drag = drag;
        }
        else if(!grounded)
        {
            rb.drag = 0;
        }
    }

    void Run()
    {
        if(running)
        {
            speed = runningSpeed;
            maxSpeed = runningSpeed;
        }     
        else if(!running)
        {
            speed = walkingSpeed;
            maxSpeed = walkingSpeed;
        }
    }

    void Jump()
    {
        if(readyJump)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

            readyJump = false;
            jumped = true;
        }
        else if(jumped && grounded)
        {
            jumped = false;
            readyJump = false;
        }
    }

    void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        if(flatVel.magnitude > maxSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * maxSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    void Camera()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        xRotation += -mouseY;
        yRotation += mouseX;

        xRotation = Mathf.Clamp(xRotation, -90, 90);

        transform.localRotation = Quaternion.Euler(0, yRotation, 0);
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }
}
