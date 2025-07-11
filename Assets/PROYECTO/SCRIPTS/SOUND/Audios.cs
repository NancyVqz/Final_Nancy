using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Audios
{
    public string name;

    public AudioClip clip;

    public bool loop;

    [Range(0, 1)]
    public float volume;

    [HideInInspector]
    public AudioSource source;
}
