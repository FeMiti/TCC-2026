using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("References")]
    private CharacterController controller;
    [SerializeField] private Transform camera;

    [SerializeField] private MinigameManager manager;

    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 10f;
    [SerializeField] private float sprintTransitionSpeed = 5f;
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private float jumpHeight = 2f;


    [Header("Mouse Look")]
    [SerializeField] private float mouseSensitivity=5f;
    private float xRotation=0f;

    private float verticalVelocity;
    private float speed;

    private bool isSprinting=false;

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
        if(manager.onMinigame) return;
        InputManagement();
        MouseLook();
        Movement();
    }

    private void Movement()
    {
        GroundMovement();
    }

    public void StartSprint()
    {
        isSprinting=true;
        speed = Mathf.Lerp(speed, sprintSpeed, sprintTransitionSpeed * Time.deltaTime);
    }

    public void StopSprint()
    {
        isSprinting=false;
        speed = Mathf.Lerp(speed, walkSpeed, sprintTransitionSpeed * Time.deltaTime);
    }

    private void GroundMovement()
    {
        Vector3 move = transform.right * turnInput + transform.forward * moveInput;

        if (isSprinting)
        {
            speed = Mathf.Lerp(speed, sprintSpeed, sprintTransitionSpeed * Time.deltaTime);
        }
        else
        {
            speed = Mathf.Lerp(speed, walkSpeed, sprintTransitionSpeed * Time.deltaTime);
        }

        move.y = 0;

        move *= speed;

        move.y = VerticalForceCalculation();

        controller.Move(move * Time.deltaTime);
    }

    private void MouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

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
}