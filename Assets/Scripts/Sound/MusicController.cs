using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioSource src;

    public void FadeMusic()
    {
        StartCoroutine(FadeRoutine());
    }

    private IEnumerator FadeRoutine()
    {
        for (int i = 0; i < 100; i++)
        {
            src.volume = 1f - (i * 0.01f);
			yield return new WaitForSeconds(0.03f);
        }
    }
}

