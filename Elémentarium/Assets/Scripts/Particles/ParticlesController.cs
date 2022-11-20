using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ParticlesController: MonoBehaviour{
    public Color paintColor;
    
    public float minRadius = 0.05f;
    public float maxRadius = 0.2f;
    public float strength = 1;
    public float hardness = 1;
    [Space]

    protected Element element;
    protected Element collidedElement;
    private Quaternion rotation;
    private bool hasCollidedOnce = false;

    [Space]
    ParticleSystem part;
    List<ParticleCollisionEvent> collisionEvents;


    void Start(){
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
        //var pr = part.GetComponent<ParticleSystemRenderer>();
        //Color c = new Color(pr.material.color.r, pr.material.color.g, pr.material.color.b, .8f);
        //paintColor = c;
    }

    private void OnEnable()
    {
        element = GetComponentInParent<Element>();
    }

    void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);
        Paintable p = other.GetComponent<Paintable>();
        BoxCollider bc = other.GetComponent<BoxCollider>();
        if (p != null && false)
        {
            for (int i = 0; i < numCollisionEvents; i++)
            {
                Vector3 pos = collisionEvents[i].intersection;
                float radius = Random.Range(minRadius, maxRadius);
                PaintManager.instance.paint(p, pos, radius, hardness, strength, paintColor);
            }
        }

        else if (bc != null && false)
        {
            bc.isTrigger = true;
            other.GetComponent<MeshRenderer>().enabled = false;
        }

        ElementCollision(other);

    }

    private void ElementCollision(GameObject other)
    {
        if (element != null)
        {
            if (collisionEvents.Count > 0 && !hasCollidedOnce)
            {
                if ((collidedElement = other.GetComponentInParent<Element>()) != null)
                {
                    rotation = Quaternion.FromToRotation(Vector3.forward, transform.forward + collidedElement.transform.forward);
                    if (collidedElement.GetPriority() > element.GetPriority()) collidedElement.GetElementData().Merge(element.GetElementData(), collisionEvents[0].intersection, rotation);
                    else element.GetElementData().Merge(collidedElement.GetElementData(), collisionEvents[0].intersection, rotation);
                    hasCollidedOnce = true;
                }
            }
        }
        else
        {
            hasCollidedOnce = false;
        }
    }
}