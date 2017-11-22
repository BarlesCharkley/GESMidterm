using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiGrav : MonoBehaviour
{
    [SerializeField]
    private float Xforce;

    [SerializeField]
    private float Yforce;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(Xforce, Yforce));
        }
    }
    

}
