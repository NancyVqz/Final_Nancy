using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Audios[] sounds;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        foreach (Audios s in sounds)
        { 
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;
            s.source.loop = s.loop;
            s.source.volume = s.volume;
        }
    }

    public void Play(string nombre)
    {
        foreach (Audios s in sounds)
        {
            if (s.name == nombre)
            {
                s.source.Play();
                return;
            }
        }
        Debug.Log("La cancion " + nombre + " no se encontro");
    }

    public void Stop(string nombre)
    {
        foreach (Audios s in sounds)
        {
            if (s.name == nombre)
            {
                s.source.Stop();
                return;
            }
        }
        Debug.Log("La cancion " + nombre + " no se encontro");
    }
}
