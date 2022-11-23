using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    public UnityEvent triggerEnterEvent;
    public UnityEvent triggerStayEvent;
    public UnityEvent triggerExitEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) triggerEnterEvent.Invoke();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")) triggerStayEvent.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) triggerExitEvent.Invoke();
    }
}
