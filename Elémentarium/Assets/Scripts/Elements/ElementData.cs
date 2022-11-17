using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ElementData : ScriptableObject
{
    
    [SerializeField] protected ID id;
    [SerializeField] protected string elementName;
    protected int priority;
    [SerializeField] protected float mass = 0;

    [Header("Particles")]
    [SerializeField] protected GameObject particlesPrefab;
    [SerializeField] protected string particlesKey;

    public abstract void Merge(ElementData elementData, Vector3 collisionPoint);
    public abstract void Remove();

    [ContextMenu("Initialize Element")]
    public void Initialize()
    {
        priority = Enum.GetValues(typeof(ID)).Cast<int>().Max() - (int)id;
    }

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
        WATER, FIRE, AIR, EARTH, STEAM, ICE, MUD, FLAMETHROWER, CLAY, SAND
    }

}
