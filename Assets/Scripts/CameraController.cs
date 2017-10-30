using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Transform objectToFollow;

    [SerializeField]
    float yOffset;

    [SerializeField]
    float xOffset;

    [SerializeField]
    float xOffsetMult;

    [SerializeField]
    float catchUpSpeed = 0.5f;

    float zOffset;
    float updatedXOffset;


    // Use this for initialization
    void Start ()
    {
        zOffset = transform.position.z;
	}
	
	// Update is called once per frame
	void Update ()
    {
        updatedXOffset = xOffset * objectToFollow.GetComponent<Rigidbody2D>().velocity.x * xOffsetMult;

        Vector3 playerPosition = 
            new Vector3(objectToFollow.position.x + updatedXOffset, 
            objectToFollow.position.y + yOffset, zOffset);

        Vector3 adjustedPosition =
            Vector3.Lerp(transform.position, playerPosition, catchUpSpeed * Time.deltaTime);

        transform.position = new Vector3(adjustedPosition.x, objectToFollow.position.y + yOffset, adjustedPosition.z);

	}
}
