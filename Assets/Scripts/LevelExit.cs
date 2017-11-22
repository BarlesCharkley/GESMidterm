using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField]
    private float loadNextSceneDelay;

    [SerializeField]
    private float settleSpeed;

    [SerializeField]
    private GameObject cakeTop;

    [SerializeField]
    private GameObject winText;

    [SerializeField]
    private GameObject pinkOutro;

    [SerializeField]
    private GameObject musicSpeaker;

    PlayerMovement player;

    private bool isCompleted = false;

    void Start ()
    {
        player = FindObjectOfType<PlayerMovement>();
        cakeTop.GetComponent<ParticleSystem>().Stop();

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            musicSpeaker.SetActive(false);
        }
    }

    void Update()
    {
        SettleOntoFrosting();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Exit();
        }
    }

    void Exit()
    {
        winText.GetComponent<FadeAlpha>().BeginFade();
        player.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        player.gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0;
        player.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        StartCoroutine(LoadNextScene());
    }

    IEnumerator LoadNextScene()
    {
        Debug.Log("Level Complete!");
        isCompleted = true;
        cakeTop.GetComponent<ParticleSystem>().Play();
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            musicSpeaker.SetActive(true);
        }

        yield return new WaitForSeconds(loadNextSceneDelay / 2);

        pinkOutro.GetComponent<FadeAlpha>().BeginFade();

        yield return new WaitForSeconds(loadNextSceneDelay / 2);

        Debug.Log("Next Level Loading...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void SettleOntoFrosting()
    {
        if (isCompleted == true)
        {
            Debug.Log("Settling");

            Vector3 adjustedPos =
                Vector3.Lerp(player.transform.position, cakeTop.transform.position, settleSpeed);

            Quaternion adjustedRot =
                Quaternion.Lerp(player.transform.rotation, cakeTop.transform.rotation, settleSpeed);

            player.transform.position = adjustedPos;

            player.transform.rotation = adjustedRot;
        }
    }
}
