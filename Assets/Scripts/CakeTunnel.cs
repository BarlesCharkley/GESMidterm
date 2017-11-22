using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakeTunnel : MonoBehaviour
{

    private Collider2D[] cakeColliders;
    private Collider2D[] tunnelColliders;

    void Start ()
    {
        cakeColliders = GetComponentsInParent<Collider2D>();
        tunnelColliders = GetComponentsInChildren<Collider2D>();
	}
	
	void Update ()
    {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            for (int i = 0; i < cakeColliders.Length; i++)
            {
                cakeColliders[i].enabled = false;
            }

            for (int i = 0; i < tunnelColliders.Length; i++)
            {
                tunnelColliders[i].enabled = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            for (int i = 0; i < cakeColliders.Length; i++)
            {
                cakeColliders[i].enabled = true;
            }

            for (int i = 0; i < tunnelColliders.Length; i++)
            {
                tunnelColliders[i].enabled = false;
            }

            GetComponent<Collider2D>().enabled = true;
        }
    }

}
