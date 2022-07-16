using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusic : MonoBehaviour
{
    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.swap)
        {
            audioSource.pitch = 1.15f;
        }
        else
        {
            audioSource.pitch = 0.85f;
        }
    }
}
