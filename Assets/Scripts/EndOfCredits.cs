using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfCredits : MonoBehaviour
{
    //[SerializeField]
    //private float exitHeight;

    [SerializeField]
    private float quitDelay;

	void Start ()
    {
        StartCoroutine(QuittinTime());
	}
	
	void Update ()
    {
        //if (GetComponent<Transform>().position.y >= exitHeight)
        //{
        //    Debug.Log("Quittin' Time");
        //    Application.Quit();
        //}
    }

    IEnumerator QuittinTime()
    {
        yield return new WaitForSeconds(quitDelay);
        Debug.Log("Quittin' Time");
        Application.Quit();

    }

}
