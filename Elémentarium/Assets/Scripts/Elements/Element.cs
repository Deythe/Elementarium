using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour
{

    [SerializeField] protected ElementData elementData;

    protected GameObject particlesGO;
    protected ParticleSystem particles;
    protected Element collidedElement;


    public void PlayParticles(Transform transform, Transform parent)
    {
        particlesGO = Pooler.instance.Pop(elementData.GetParticlesKey(), transform.position, parent);
        particles = particlesGO.GetComponent<ParticleSystem>();
        if (particles != null) 
        {
            particles.Play();
        }
    }

    public void StopParticles()
    {
        if (particles != null) 
        {
            particles.Stop();
        }

        Pooler.instance.DePop(elementData.GetParticlesKey(), particlesGO);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((collidedElement = collision.transform.GetComponent<Element>()) != null)
        {
            if (collidedElement.GetPriority() > GetPriority()) collidedElement.GetElementData().Merge(elementData);
            else elementData.Merge(collidedElement.GetElementData());

            elementData.Remove();
            collidedElement.GetElementData().Remove();
        }
    }

    public ElementData GetElementData() 
    {
        return elementData;
    }

    public float GetMass()
    {
        return elementData.GetMass();
    }

    public ElementData.ID GetID()
    {
        return elementData.GetID();
    }

    public int GetPriority() 
    {
        return elementData.GetPriority();
    }
}
