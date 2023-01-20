using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Lever : MonoBehaviour
{
    [SerializeField] private Transform lever, leverUp, leverDown;
    [SerializeField] private UnityEvent onEvent;
    [SerializeField] private UnityEvent offEvent;
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip openClip, closeClip;
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.Equals(lever))
        {
            if (Vector3.Distance(lever.position, leverUp.position) <
                Vector3.Distance(lever.position, leverDown.position))
            {
                Debug.Log("Up");
                source.clip = openClip;
                source.Play();
                onEvent.Invoke();
            }
            else
            {
                Debug.Log("Down");
                source.clip = closeClip;
                source.Play();
                offEvent.Invoke();
            }
        }
    }
}
