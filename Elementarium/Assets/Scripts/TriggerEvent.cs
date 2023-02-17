using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    [SerializeField] private List<Transform> transforms;

    public UnityEvent triggerEnterEvent;
    public UnityEvent triggerStayEvent;
    public UnityEvent triggerExitEvent;
    public bool thisResetsElements;

    private bool canInvoke = false;

    private void OnTriggerEnter(Collider other)
    {
        if (thisResetsElements)
        {
            PlayerManager playerManager = null;
            if ((playerManager = other.gameObject.GetComponentInChildren<PlayerManager>()) != null)
            {
                playerManager.ResetElements();
                triggerEnterEvent.Invoke();
                return;
            }
            return;
        }
        
        foreach(Transform t in transforms) 
        {
            if (other.transform.Equals(t)) canInvoke = true;
        }

        if (canInvoke) triggerEnterEvent.Invoke();
        canInvoke = false;
    }

    private void OnTriggerStay(Collider other)
    {
        foreach (Transform t in transforms)
        {
            if (other.transform.Equals(t)) canInvoke = true;
        }

        if (canInvoke) triggerStayEvent.Invoke();
        canInvoke = false;
    }

    private void OnTriggerExit(Collider other)
    {
        foreach (Transform t in transforms)
        {
            if (other.transform.Equals(t)) canInvoke = true;
        }

        if (canInvoke) triggerExitEvent.Invoke();
        canInvoke = false;
    }
}
