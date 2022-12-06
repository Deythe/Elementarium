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
        if(GetComponentInParent<HandPresencePhysics>()==null) return;
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
    }
}