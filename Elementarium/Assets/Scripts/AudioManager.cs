using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    
    [Serializable]
    public struct Clip
    {
        public AudioClip audioClip;
        public bool isLooping;
    }

    [SerializeField] private List<AudioSource> _listAudioSourc = new List<AudioSource>();

    private void Awake()
    {
        instance = this;
    }

    public void PlayPist(Clip aud)
    {
        foreach (AudioSource audioS in _listAudioSourc)
        {
            if (!audioS.isPlaying)
            {
                audioS.clip = aud.audioClip;
                audioS.loop = aud.isLooping;
                audioS.Play();
                return;
            }
        }
    }

    public void StopPist(Clip aud)
    {
        foreach (AudioSource audioS in _listAudioSourc)
        {
            if (audioS.clip!=null && audioS.clip.Equals(aud.audioClip))
            {
                audioS.Stop();
                audioS.clip = null;
                audioS.loop = false;
                return;
            }
        }
    }
}
