using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Sound[] sounds;

    private static AudioManager _instance;

    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<AudioManager>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(AudioManager).Name);
                    _instance = singletonObject.AddComponent<AudioManager>();
                }
            }

            return _instance;
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        /*foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;
            s.source.outputAudioMixerGroup = s.mixerGroup;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }*/
    }

    public bool RequestAddAudio(string _name, EntityAudioController _entity)
    {
        //Get sound
        Sound sound = FindSound( _name );
        //Sound exists
        if (sound != null)
        {
            //create new sound
            SoundEntity newSoundEntity = new SoundEntity();
            newSoundEntity.name = _name;
            //create new audiosource
            newSoundEntity.source = _entity.gameObject.AddComponent<AudioSource>();
            newSoundEntity.source.clip = sound.clip;
            newSoundEntity.source.outputAudioMixerGroup = sound.mixerGroup;
            newSoundEntity.source.pitch = sound.pitch;
            newSoundEntity.source.loop = sound.loop;
            newSoundEntity.source.spatialBlend = sound.is3D ? 1 : 0;
            newSoundEntity.source.maxDistance = 10;
            //volume
            newSoundEntity.source.volume = sound.volume;
            //add to _entity
            _entity.AddSoundEntity(newSoundEntity);
            return true;
        }

        return false;
    }

    private Sound FindSound(string name)
    {
        foreach (Sound s in sounds)
        {
            if (s.name == name)
                return s;
        }
        return null;
    }

    //try to play audio
    public bool Play(string _soundName, List<SoundEntity> _sounds, bool willStopPrev)
    {
        //Find audio in list
        foreach (var sound in _sounds)
        {
            //check if same sound
            if (sound.name == _soundName)
            {
                //play sound
                PlayAudio(sound.source, willStopPrev);
                return true;
            }
        }
        return false;
    }

    void PlayAudio(AudioSource _audioSource, bool willStopPrev)
    {
        //check if still playing
        if (_audioSource.isPlaying)
        {
            if (!willStopPrev)
                return;
            //stop playing
            _audioSource.Stop();
        }
        _audioSource.Play();
    }

    /*public void OnlyPlayAfterSoundEnds(string sound)
    {
        foreach (Sound s in sounds)
        {
            if (s.name == sound && !s.source.isPlaying)
                s.source.Play();
        }
    }*/

    /*public void Stop(string sound)
    {
        FindSound(sound).source.Stop();
    }*/

    /*public void StopAllSounds()
    {
        foreach (Sound s in sounds)
            s.source.Stop();
    }*/

    /*public void Pause(string sound)
    {
        FindSound(sound).source.Pause();
    }*/

    /*public void Unpause(string sound)
    {
        FindSound(sound).source.UnPause();
    }*/

    /*public void PauseAllSounds()
    {
        foreach (Sound s in sounds)
            s.source.Pause();
    }*/

    /*public void UnpauseAllSounds()
    {
        foreach (Sound s in sounds)
            s.source.UnPause();
    }*/

    /*public bool CheckIfSoundPlaying(string sound)
    {
        return FindSound(sound).source.isPlaying;
    }*/

    // Fade in/out all sounds within set duration to targetVolume
    /*public void FadeAllSound(bool fadeIn, float duration, float targetVolume)
    {
        foreach(Sound s in sounds)
        {
            StartCoroutine(FadeOutSound(fadeIn, s, duration, targetVolume));
        }
    }*/

    // Fade in/out sound within set duration to targetVolume
    /*public void FadeSound(bool fadeIn, string sound, float duration, float targetVolume)
    {
        foreach (Sound s in sounds)
        {
            if (s.name == sound)
                StartCoroutine(FadeOutSound(fadeIn, s, duration, targetVolume));
        }
    }*/

    // Reset sound volume to 1
    /*private void ResetVolumeOfSound(Sound sound)
    {
        foreach(Sound s in sounds)
        {
            if (s.name == sound.name)
                s.source.volume = 1;
        }
    }*/

    // Play sound after delay
    /*public void PlayAfterDelay(float delay, string sound)
    {
        foreach (Sound s in sounds)
        {
            if (s.name == sound)
                s.source.PlayDelayed(delay);
        }
    }*/

    /*private IEnumerator FadeOutSound(bool fadeIn, Sound sound, float fadeDuration, float targetVolume)
    {
        float time = 0f;
        float startVolume = sound.source.volume;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            if (!fadeIn)
                sound.source.volume = Mathf.Lerp(startVolume, targetVolume, time / fadeDuration);
            else
                sound.source.volume = Mathf.Lerp(0, targetVolume, time / fadeDuration);
            yield return null;
        }

        sound.source.Stop();
        ResetVolumeOfSound(sound);

        yield break;
    }*/
}