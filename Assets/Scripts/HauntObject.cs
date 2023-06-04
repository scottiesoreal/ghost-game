using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HauntObject : MonoBehaviour
{
    [SerializeField] 
    private float hauntDistance = 5f; // The maximum distance at which the player can interact with the object.
    [SerializeField] 
    private float bumpForce = 2f; // The force with which the object will be bumped.
    [SerializeField] 
    private float bumpDistance = 1f; // The distance the object will move when bumped.
    [SerializeField] 
    private Player player; // Reference to the Player object.
    private Vector3 originalPosition; // Original position of the object

    void Start()
    {
        originalPosition = transform.position; // Record the original position of the object
    }

    void Update()
    {
        // Calculate the distance between the player and the object
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        Debug.Log("Distance to Player: " + distanceToPlayer);

        // If the player is within the haunt range
        if (distanceToPlayer <= hauntDistance && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Player Interacting with object.");
            StartCoroutine(Bump());
        }
    }

    private IEnumerator Bump()
    {
        // Bump the object
        Vector3 bumpDirection = (transform.position - player.transform.position).normalized;
        Vector3 bumpPosition = transform.position + bumpDirection * bumpDistance;
        Debug.Log("Bump Position: " + bumpPosition);
        float bumpTime = 0f;
        Vector3 startingPosition = transform.position;

        while (bumpTime < 1f)
        {
            transform.position = Vector3.Lerp(startingPosition, bumpPosition, bumpTime);
            bumpTime += Time.deltaTime * bumpForce;
            yield return null;
        }

        // Reset the object position
        bumpTime = 0f;
        while (bumpTime < 1f)
        {
            transform.position = Vector3.Lerp(bumpPosition, originalPosition, bumpTime);
            bumpTime += Time.deltaTime * bumpForce;
            yield return null;
        }

        transform.position = originalPosition;
    }
}
