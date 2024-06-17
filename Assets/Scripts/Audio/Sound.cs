using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    public AudioMixerGroup mixerGroup;

    [Range(0.1f, 3f)]
    public float pitch = 1;
    [Range(0f, 1f)]
    public float volume = 0.5f;

    /*[HideInInspector]
    public AudioSource source;*/

    public bool loop;

    public bool is3D = false;
}


[System.Serializable]
public class SoundEntity
{
    public string name;
    [HideInInspector]
    public AudioSource source;

}