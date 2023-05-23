using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float ascentSpeed = 5f;
    [SerializeField] private float descentSpeed = 5f;
    
    [SerializeField] private bool isGravityEnabled = true;

    private Rigidbody rb;

    private string horizontalAxis = "Horizontal";
    private string verticalAxis = "Vertical";
    private string ascendButton = "Ascend";
    private string descendButton = "Descend";

    private bool isUsingGamepad = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Check if gamepad is connected
        isUsingGamepad = Input.GetJoystickNames().Length > 0;

        // Movement
        float horizontalInput = Input.GetAxis(horizontalAxis);
        float verticalInput = Input.GetAxis(verticalAxis);

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * movementSpeed;
        rb.velocity = movement;

        // Rotation
        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement);
            rb.rotation = Quaternion.Slerp(rb.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        // Ascend and Descend
        float ascendInput = Input.GetAxis("Ascend");
        float descendInput = Input.GetAxis("Descend");

        if (ascendInput > 0)
        {
            Debug.Log("Ascend");
            Vector3 ascent = Vector3.up * ascendInput * ascentSpeed;
            rb.velocity += ascent;
        }
        else if (descendInput > 0)
        {
            Debug.Log("Descend");
            Vector3 descent = Vector3.down * descendInput * descentSpeed;
            rb.velocity += descent;
        }
    }
}