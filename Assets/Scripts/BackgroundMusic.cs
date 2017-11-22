using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{

    private int speakerCount;

    void Awake()
    {
        speakerCount = FindObjectsOfType<BackgroundMusic>().Length;

        if (speakerCount == 1)
        {
            DontDestroyOnLoad(transform.gameObject);
        }
    }

}