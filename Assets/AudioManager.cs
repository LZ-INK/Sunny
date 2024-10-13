using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
 class AudioManager : MonoSingleton<MonoBehaviour>
{
    AudioSource audio;
    protected override void OnStart()
    {
        audio = GetComponent<AudioSource>();
        audio.Play();
    }
}
