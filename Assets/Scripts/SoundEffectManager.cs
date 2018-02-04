using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : Singleton<SoundEffectManager> {
    AudioSource source;
    public AudioClip rotateSfx;
    public AudioClip cantActSfx;

    void Awake() {
        source = GetComponent<AudioSource>();
         DontDestroyOnLoad(gameObject);
    }

    public void PlayRotate() {
        source.clip = rotateSfx;
        source.Play();
    }

    public void PlayCantAct() {
        source.clip = cantActSfx;
        source.Play();
    }
}
