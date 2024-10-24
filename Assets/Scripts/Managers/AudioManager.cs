using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [Serializable]
    private class SoundIDClipPair
    {
        public SoundID SoundID;
        public AudioClip AudioClip;
    }

    [SerializeField] private SoundIDClipPair[] soundIDClipPairs;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource effectSource;

    private readonly Dictionary<SoundID, AudioClip> soundIDToClipMap = new();

    private void Start()
    {
        InitializeSoundMap();
    }

    private void InitializeSoundMap()
    {
        foreach (var pair in soundIDClipPairs)
        {
            if (!soundIDToClipMap.ContainsKey(pair.SoundID))
            {
                soundIDToClipMap[pair.SoundID] = pair.AudioClip;
            }
        }
    }

    public void PlayEffect(SoundID soundID)
    {
        if (soundID == SoundID.None) return;

        if (soundIDToClipMap.TryGetValue(soundID, out var clip))
        {
            effectSource.PlayOneShot(clip);
        }
    }
    
}