using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionEvent : MonoBehaviour
{
    [SerializeField] private string tag;
    [SerializeField] private UnityEvent collisionEnterEvent;
    [SerializeField] private UnityEvent collisionStayEvent;
    [SerializeField] private UnityEvent collisionExitEvent;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag(tag)) 
        {
            collisionEnterEvent.Invoke();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.CompareTag(tag))
        {
            collisionStayEvent.Invoke();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag(tag)) 
        {
            collisionExitEvent.Invoke();
        }
    }
}
