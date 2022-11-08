using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Element : MonoBehaviour
{
    protected enum ID {WATER, FIRE, AIR, EARTH, STEAM, ICE, MUD, FLAMETHROWER, CLAY, SAND};
    [SerializeField] protected string elementName;
    protected int priority;
    protected int id;
    [SerializeField] protected float mass = 0;
    [SerializeField] protected ParticleSystem particles;

    private Element collidedElement;

    protected virtual void Start()
    {
        mass = 0;
    }

    protected abstract void Merge(Element element);
    protected abstract void Remove();

    public void PlayParticles() 
    {
        particles.Play();
    }

    public void StopParticles() 
    {
        particles.Stop();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((collidedElement = collision.transform.GetComponent<Element>()) != null)
        {
            if (collidedElement.priority > priority) collidedElement.Merge(this);
            else Merge(collidedElement);

            Remove();
            collidedElement.Remove();
        }
    }

    public float GetMass()
    {
        return mass;
    }

    public int GetID()
    {
        return id;
    }
}
