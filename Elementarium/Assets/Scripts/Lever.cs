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

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.Equals(lever))
        {
            if (Vector3.Distance(lever.position, leverUp.position) <
                Vector3.Distance(lever.position, leverDown.position))
            {
                Debug.Log("Up");
                onEvent.Invoke();
            }
            else
            {
                Debug.Log("Down");
                offEvent.Invoke();
            }
        }
    }
}
