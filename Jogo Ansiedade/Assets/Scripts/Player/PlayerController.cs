using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("References")]
    private CharacterController controller;
    [SerializeField] private Transform camera;

    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 10f;
    [SerializeField] private float sprintTransitionSpeed = 5f;
    [SerializeField] private float turningSpeed = 2f;
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private Animator animador;
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip[] clips;
    //[SerializeField] private PauseScript pause;


    [Header("Mouse Look")]
    [SerializeField] private float mouseSensitivity=100f;
    private float xRotation=0f;

    private float verticalVelocity;
    private float speed;

    [Header("Input")]
    private float moveInput;
    private float turnInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void TeleportPlayer(Vector3 localTeleportado)
    {
        controller.enabled=false;
        transform.position=localTeleportado;
        controller.enabled=true;
    }

    // Update is called once per frame
    private void Update()
    {
        //if(pause.isPaused)
            //return;
        InputManagement();
        MouseLook();
        Movement();
    }

    private void Movement()
    {
        GroundMovement();
    }

    private void GroundMovement()
    {
        Vector3 move = transform.right * turnInput + transform.forward * moveInput;

        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            animador.SetBool("isWalking",true);
            if (Input.GetKey(KeyCode.W))
            {
                animador.SetInteger("direction",0);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                animador.SetInteger("direction",1);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                animador.SetInteger("direction",2);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                animador.SetInteger("direction",3);
            }
        }
        else
        {
            animador.SetBool("isWalking",false);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = Mathf.Lerp(speed, sprintSpeed, sprintTransitionSpeed * Time.deltaTime);
            animador.SetBool("isRunning",true);
        }
        else
        {
            speed = Mathf.Lerp(speed, walkSpeed, sprintTransitionSpeed * Time.deltaTime);
            animador.SetBool("isRunning",false);
        }

        move.y = 0;

        move *= speed ;

        move.y = VerticalForceCalculation();

        controller.Move(move * Time.deltaTime);
    }

    private void MouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        camera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    private float VerticalForceCalculation()
    {
        if (controller.isGrounded)
        {
            verticalVelocity = -1f;

            if (Input.GetButtonDown("Jump"))
            {
                PlayClip(0);
                verticalVelocity = Mathf.Sqrt(jumpHeight * gravity * 2);
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }
        return verticalVelocity;
    }
    
    private void InputManagement()
    {
        moveInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");
    }

    private void PlayClip(int clip)
    {
        source.PlayOneShot(clips[clip]);
    }
}