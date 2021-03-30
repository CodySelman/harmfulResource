using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleAudio : MonoBehaviour
{
    public AudioSource sfxEnterSource;

    public void playEnterSource() {
        float ranPitch = Random.Range(0.96f, 1.04f);
        sfxEnterSource.pitch = ranPitch;
        sfxEnterSource.Play();
    }
}
