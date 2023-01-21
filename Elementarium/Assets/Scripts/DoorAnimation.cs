using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimation : MonoBehaviour
{
    [SerializeField] private AudioClip doorOpenSound, doorCloseSound;
    [SerializeField] private AudioSource source;
    [SerializeField] Animation doorState;
    [SerializeField] bool open;

    void Start()
    {
        if (open)
        {
            open = false;
            Open();
        }
    }

    public void Open()
    {
        if(!open) doorState.Play("DoorOpen");
        if (!source.isPlaying)
        {
            source.clip = doorOpenSound;
            source.Play();
        }

        open = true;
    }

    public void Close()
    {
        if(open) doorState.Play("DoorClose");
        if (source.isPlaying)
        {
            source.clip = doorCloseSound;
            source.Stop();
        }

        open = false;
    }
}
