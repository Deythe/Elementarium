using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class CollisionEvent : MonoBehaviour
{
    [SerializeField] private string thisTag;
    [SerializeField] private UnityEvent collisionEnterEvent;
    [SerializeField] private UnityEvent collisionStayEvent;
    [SerializeField] private UnityEvent collisionExitEvent;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag(thisTag)) 
        {
            collisionEnterEvent.Invoke();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.CompareTag(thisTag))
        {
            collisionStayEvent.Invoke();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag(thisTag)) 
        {
            collisionExitEvent.Invoke();
        }
    }
}
