using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Audio
{
    public enum AudioType { soundEffect, music }
    public AudioType audiotype;
    [HideInInspector] public AudioSource source;
    public AudioClip audioclip;
    public string clipname;
    public bool isloop;
    public bool playonawake;

    [Range(0, 1)]
    public float volume = 0.5f;
}
