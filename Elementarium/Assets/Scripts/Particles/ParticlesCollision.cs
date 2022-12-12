using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesCollision : MonoBehaviour
{
    private List<ParticleCollisionEvent> collisionEvents;
    private ParticleSystem part;

    protected Element element;
    protected Element collidedElement;
    private Quaternion rotation;
    private bool canElementCollide = true;
    private float collisionCooldown = 0.1f;

    private void Start()
    {
        canElementCollide = true;
        collisionEvents = new List<ParticleCollisionEvent>();
        part = GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        canElementCollide = true;
        element = GetComponentInParent<HandPresencePhysics>().target.GetComponent<Element>();
    }

    private void OnParticleCollision(GameObject other)
    {
        part.GetCollisionEvents(other, collisionEvents);
        ElementCollision(other);
    }

    private void ElementCollision(GameObject other)
    {
        if (element != null)
        {
            if (collisionEvents.Count > 0 && canElementCollide)
            {
                if (other.GetComponentInParent<HandPresencePhysics>() == null) return;
                if ((collidedElement = other.GetComponentInParent<HandPresencePhysics>().target.GetComponent<Element>()) != null)
                {
                    //Debug.Log("1");
                    //Debug.Log(collidedElement.GetID() + " / " + element.GetID());
                    if (collidedElement.GetID() != element.GetID()) 
                    {
                        rotation = Quaternion.FromToRotation(Vector3.forward, transform.forward + collidedElement.transform.forward);
                        //Debug.Log("collided element " + collidedElement.GetID() + " / priority " + collidedElement.GetPriority() + "\nelement " + element.GetID() + " / element priority " + element.GetPriority());
                        if (collidedElement.GetPriority() > element.GetPriority())
                        {
                            collidedElement.GetElementData().Merge(element.GetElementData(), collisionEvents[0].intersection, rotation);
                        }
                        else
                        {
                            element.GetElementData().Merge(collidedElement.GetElementData(), collisionEvents[0].intersection, rotation);
                        }
                        canElementCollide = false;
                        StartCoroutine(CollideCoroutine(collisionCooldown));
                    }
                }
            }
        }
    }

    IEnumerator CollideCoroutine(float t)
    {
        //Debug.Log("Start Coroutine");
        yield return new WaitForSeconds(t);

        canElementCollide = true;
    }
}
