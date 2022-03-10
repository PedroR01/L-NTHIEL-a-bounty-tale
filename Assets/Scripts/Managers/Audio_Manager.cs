using UnityEngine.Audio;
using System;
using UnityEngine;

public class Audio_Manager : MonoBehaviour
{
    public Sound_Assets[] sounds;
    public static Audio_Manager soundInstance;
    /// <summary>
    /// Borrar el audio manager y que cada objeto maneje su propio sonido
    /// </summary>

    private void Awake()
    {
        if (soundInstance != null && soundInstance != this)
        {
            Destroy(gameObject);
            return;
        }

        soundInstance = this;
        DontDestroyOnLoad(gameObject);

        foreach (Sound_Assets _sounds in sounds)
        {
            _sounds.source = gameObject.AddComponent<AudioSource>();
            _sounds.source.clip = _sounds.clip;
            _sounds.source.volume = _sounds.volume;
            _sounds.source.pitch = _sounds.pitch;
            _sounds.source.loop = _sounds.loop;
        }
    }

    public void Play(string name)
    {
        Sound_Assets theSound = Array.Find(sounds, Sound_Assets => Sound_Assets.name == name);
        if (theSound == null)
        {
            Debug.LogWarning("Wrong sound name");
            return;
        }
        theSound.source.Play();
    }

    public void RandomSoundValues(float randomValue)
    {
    }
}