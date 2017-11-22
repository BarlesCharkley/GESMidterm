using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hazard : MonoBehaviour
{
    [SerializeField]
    private float resetDelay;

    FailScreen failScreen;

    PlayerMovement player;

	void Start ()
    {
        failScreen = FindObjectOfType<FailScreen>();
        player = FindObjectOfType<PlayerMovement>();
	}

    private void OnTriggerEnter2D(Collider2D other2D)
    {
        if (other2D.tag == "Player")
        {
            Die();
        }
    }

    public void Die()
    {
        failScreen.BeginFade();
        player.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        player.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        player.gameObject.GetComponentInChildren<Animator>().SetTrigger("Die");
        StartCoroutine(Reload());
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(resetDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
