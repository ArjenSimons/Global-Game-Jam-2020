using UnityEngine.Audio;
using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
/// <summary>
/// This script manages the audio that is saved in the soundsArray
/// </summary>
public class AudioManager : MonoBehaviour
{
    // add sounds to the array in the inspector
    public Sound[] soundsArray;
    private float timer = 0.0f;
    private float timer2 = 0.0f;
    private List<AudioSource> randomAudioClips;
    private List<AudioSource> randomAudioClipsInterval;
    private int chooseRandomAudio;

    private void Awake()
    {
        // loop through the sounds in the array
        foreach (Sound s in soundsArray)
        {
            // each sound will get an AudioSource component
            // this component will be created in the AudioManager GameObject
            s.soundSource = gameObject.AddComponent<AudioSource>();

            // customization of the component
            s.soundSource.clip = s.clip;
            s.soundSource.volume = s.volume;
            s.soundSource.pitch = s.pitch;
            s.soundSource.loop = s.loop;
            s.soundSource.playOnAwake = s.playOnAwake;
        }
    }

    private void Start()
    {
    }

    private void Update()
    {
        timer += Time.deltaTime;
        timer2 += Time.deltaTime;
    }

    // method to play a sound
    public void Play(string name)
    {
        // find a sound in the array that has the name of name
        // and store the sound that it found in s
        Sound s = Array.Find(soundsArray, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound clip: " + name + " not found.");
        }
        if (!s.soundSource.isPlaying)
        {
            s.soundSource.Play();
        }
    }

    // method to play a sound
    public void PlayOneShot(string name)
    {
        // find a sound in the array that has the name of name
        // and store the sound that it found in s
        Sound s = Array.Find(soundsArray, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound clip: " + name + " not found.");
        }
        if (!s.soundSource.isPlaying)
        {
            s.soundSource.Play();
            s.soundSource.PlayOneShot(s.soundSource.clip, 0.2f);
        }
    }

    // method to play a sound
    public void Stop(string name)
    {
        // find a sound in the array that has the name of name
        // and store the sound that it found in s
        Sound s = Array.Find(soundsArray, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound clip: " + name + " not found.");
        }
        if (s.soundSource.isPlaying)
        {
            s.soundSource.Stop();
        }
    }

    public void PlayRandom(string containingInName)
    {
        // making new list of audioclips that should play at random
        randomAudioClips = new List<AudioSource>();

        //add the audioclips that contain the "containingInName" parameter in their name, to the list
        foreach (Sound s in soundsArray)
        {
            if (s.name.Contains(containingInName))
                randomAudioClips.Add(s.soundSource);
        }

        int randomAudio = UnityEngine.Random.Range(0, randomAudioClips.Count);
        randomAudioClips[randomAudio].Play();
    }

    // method to play a sound on intervals
    public void PlayAtInterval(string name, float interval)
    {
        Sound s = Array.Find(soundsArray, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound clip: " + name + " not found.");
        }

        float timeInterval = interval;

        if (timer >= timeInterval)
        {
            // Play the sound, reset the timer
            s.soundSource.Play();
            timer = 0.0f;
        }
    }

    // method to play RANDOM sounds on interval
    public void PlayAtIntervalRandom(string containingInName, float interval)
    {
        // making new list of audioclips that should play at random
        randomAudioClipsInterval = new List<AudioSource>();

        //add the audioclips that contain the "containingInName" parameter in their name, to the list
        foreach (Sound s in soundsArray)
        {
            if (s.name.Contains(containingInName))
                randomAudioClipsInterval.Add(s.soundSource);
        }

        float timeInterval = interval;
        int randomAudio = UnityEngine.Random.Range(0, randomAudioClipsInterval.Count);

        if (timer2 >= timeInterval)
        {
            // Play the sound, reset the timer
            randomAudioClipsInterval[randomAudio].Play();
            timer2 = 0.0f;
        }
    }
}
