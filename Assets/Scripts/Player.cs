using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] 
    private float speed = 5f;
    private Rigidbody playerRigidbody;
    [SerializeField] 
    private bool isAscending = false;
    [SerializeField] 
    private bool isDescending = false;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        CalculateMovement();
        
        // Check for ascent input
        if (Input.GetKeyDown(KeyCode.V))
        {
            isAscending = true;
            playerRigidbody.useGravity = false; // Disable gravity
        }
        else if (Input.GetKeyUp(KeyCode.V))
        {
            isAscending = false;
        }

        // Check for descent input
        if (Input.GetKeyDown(KeyCode.C))
        {
            isDescending = true;
        }
        else if (Input.GetKeyUp(KeyCode.C))
        {
            isDescending = false;
        }

              
    }

    void CalculateMovement()
    {
        // Get input axes
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        float moveY = isAscending ? 0.5f : (isDescending ? -0.5f : 0f);

        // Calculate movement vector
        Vector3 movement = new Vector3(moveHorizontal, moveY, moveVertical);
        movement = transform.TransformDirection(movement) * speed * Time.deltaTime;

        // Apply movement to the player
        MovePlayer(movement);
    }

    private void MovePlayer(Vector3 movement)
    {
        // Apply movement to the player
        transform.position += movement;
    }
}
