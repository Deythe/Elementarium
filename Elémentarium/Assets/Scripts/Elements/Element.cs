using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Element : MonoBehaviour
{
    [SerializeField] protected string elementName;
    protected int id;
    protected int priority;
    [SerializeField] protected float mass;
    [SerializeField] protected ParticleSystem particles;

    private Element collidedElement;

    protected abstract void Merge(Element element);

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
