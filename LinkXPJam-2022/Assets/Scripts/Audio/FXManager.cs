using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXManager : MonoBehaviour
{
    private void Awake()
    {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        }
        else {
            instance = this;
        }
    }
    public static FXManager instance;

    [SerializeField] float vol = 0.1f;
    List<AudioSource> sources = new();

    public void PlaySound(AudioClip clip)
    {
        AudioSource source = GetAvailableSource();
        source.clip = clip;
        source.Play();
    }

    AudioSource GetAvailableSource()
    {
        foreach (AudioSource source in sources) {
            if (!source.isPlaying) { return source; }
        }
        //no available source found, make new one
        AudioSource newSource = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        ConfigureSource(newSource);
        return newSource;
    }

    void ConfigureSource(AudioSource source)
    {
        source.volume = vol;
        source.loop = false;
        source.playOnAwake = false;
    }
}
