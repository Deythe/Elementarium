using System;
using System.Linq;
using UnityEngine;

public abstract class ElementData : ScriptableObject
{
    
    [SerializeField] protected ID id;
    [SerializeField] protected string elementName;
    protected int priority;
    [SerializeField] protected float mass = 0;

    [SerializeField] private AudioClip expulseAudioClip;
    
    [Header("Particles")]
    [SerializeField] protected GameObject particlesPrefab;
    [SerializeField] protected string particlesKey;
    [SerializeField] private Material matBracelet;
    
    public abstract void Merge(Transform elementCollided, ElementData elementData, Vector3 collisionPoint, Quaternion collisionRotation);
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
        WATER, FIRE, AIR, STEAM, ICE, FLAMETHROWER
    }

    public AudioClip GetAudioClip()
    {
        return expulseAudioClip;
    }

    public Material GetMatBracelet()
    {
        return matBracelet;
    }
}
