using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckFollow : MonoBehaviour {

    [SerializeField]
    Transform player;

    [SerializeField]
    private float offset;

	void Start ()
    {
		
	}
	
	void Update ()
    {
        Follow();
	}

    void Follow()
    {
        Vector3 playerPos = player.position;
        gameObject.transform.position = new Vector3 (playerPos.x, (playerPos.y - offset), playerPos.z);
    }
}
