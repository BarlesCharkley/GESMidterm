using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private float respawnDelay;

    PlayerMovement playerScript;
    private AudioSource speaker;

    private void Start()
    {
        playerScript = FindObjectOfType<PlayerMovement>();
        speaker = gameObject.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(CoinPickup());
        }
    }

    IEnumerator CoinPickup()
    {
        speaker.Play();
        playerScript.coinCount++;
        playerScript.GivePowerup();
        Debug.Log(playerScript.coinCount + " collected");
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<Collider2D>().enabled = false;

        yield return new WaitForSeconds(respawnDelay);

        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<Collider2D>().enabled = true;
    }

}
