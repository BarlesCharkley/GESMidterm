using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    PlayerMovement playerScript;

    private void Start()
    {
        playerScript = FindObjectOfType<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerScript.coinCount++;
            Debug.Log(playerScript.coinCount + " collected");

            playerScript.GivePowerup();

            gameObject.SetActive(false);
        }
    }

}
