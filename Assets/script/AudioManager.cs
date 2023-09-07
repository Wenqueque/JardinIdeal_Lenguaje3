using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public List<AudioSource> audioSources = new List<AudioSource>();

    // M�todo para reproducir un sonido en un AudioSource espec�fico
    public void PlaySound(int index)
    {
        if (index >= 0 && index < audioSources.Count)
        {
            audioSources[index].Play();
        }
    }
}

