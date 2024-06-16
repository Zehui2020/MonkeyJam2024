using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAudioController : MonoBehaviour
{
    //hold all sounds
    private List<SoundEntity> sounds = new List<SoundEntity>();
    
    public void PlayAudio(string  audioName)
    {
        //playaudio
        if(!AudioManager.Instance.Play(audioName, sounds))
        {
            //failed to play audio
            //add audio
            if (AudioManager.Instance.RequestAddAudio(audioName, this))
            {
                //request to play audio if audio exists
                PlayAudio(audioName);
            }
        }
    }

    public void AddSoundEntity(SoundEntity sound)
    {
        sounds.Add(sound);
    }

    public void StopAudio(string audioName)
    {
        //check if can find audio with same name
        foreach (var sound in sounds)
        {
            //check audio name
            if (audioName == sound.name)
            {
                //stop
                sound.source.Stop();
            }
        }
    }
}
