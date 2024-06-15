using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerPrefs")]
public class PlayerPrefs : ScriptableObject
{
    public float masterVolume = 1;
    public float bgmVolume = 1f;
    public float sfxVolume = 0.5f;

    public void ResetVolume()
    {
        masterVolume = 1;
        bgmVolume = 1f;
        sfxVolume = 0.5f;
    }
}