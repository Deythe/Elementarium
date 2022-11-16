using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ElementData : ScriptableObject
{
    
    [SerializeField] protected ID id;
    [SerializeField] protected string elementName;
    //[SerializeField] protected int id;
    [SerializeField] protected int priority;
    [SerializeField] protected float mass = 0;

    [Header("Particles")]
    [SerializeField] protected GameObject particlesPrefab;
    [SerializeField] protected string particlesKey;

    public abstract void Merge(ElementData elementData);
    public abstract void Remove();

    public string GetName() 
    {
        return this.elementName;
    }

    public int GetPriority()
    {
        return this.priority;
    }

    public ID GetID() 
    {
        return this.id;
    }

    public float GetMass() 
    {
        return this.mass;
    }

    public GameObject GetParticlesPrefab() 
    {
        return this.particlesPrefab;
    }

    public string GetParticlesKey() 
    {
        return this.particlesKey;
    }

    public enum ID 
    {
        WATER, FIRE, AIR, EARTH
    }

}
