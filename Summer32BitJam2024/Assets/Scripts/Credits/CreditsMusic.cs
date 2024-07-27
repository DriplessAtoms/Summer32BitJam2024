using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsMusic : MonoBehaviour
{
    public AudioSource audioBegin;
    public AudioSource audioLoop;

    private bool startedLoop;

    void Start()
    {
        startedLoop = false;
    }

    void Update()
    {
        if (audioBegin.time > 13.8f && !startedLoop)
        {
            startedLoop = false;
            audioLoop.loop = true;
            audioLoop.Play();
        }
    }
}
