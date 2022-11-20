using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour
{

    [SerializeField] protected ElementData elementData;

    protected GameObject particlesGO;
    protected ParticleSystem particles;
    protected Element collidedElement;
    protected List<ParticleCollisionEvent> particlesCollisions;

    protected Quaternion rotation;

    private void Start()
    {
        if (elementData != null)
        {
            elementData.Initialize();
        }
        particlesCollisions = new List<ParticleCollisionEvent>(); 
    }

    public void PlayParticles()
    {
        particlesGO = Pooler.instance.Pop(elementData.GetParticlesKey(), transform.position, transform);
        if ((particles = particlesGO.GetComponent<ParticleSystem>()) != null)
        {
            particles.Play();
        }
    }

    public void PlayParticles(Transform t, Transform parent)
    {
        particlesGO = Pooler.instance.Pop(elementData.GetParticlesKey(), t.position, parent);
        if ((particles = particlesGO.GetComponent<ParticleSystem>()) != null) 
        {
            particles.Play();
        }
    }

    public void StopParticles()
    {
        if (particles != null && !particles.IsAlive()) 
        {
            particles.Stop();
        }

        if (particlesGO != null)
        {
            Pooler.instance.DePop(elementData.GetParticlesKey(), particlesGO);
            particlesGO = null;
        }
    }

    public void DelayedStopParticles(float t) 
    {
        StartCoroutine(StopParticlesCoroutine(t));
    }

    IEnumerator StopParticlesCoroutine(float t) 
    {
        while (!particles.isStopped)
        {

            yield return new WaitForSeconds(t);

            if (particles != null && !particles.IsAlive())
            {
                particles.Stop();
            }
        }

        if (particlesGO != null)
        {
            Pooler.instance.DePop(elementData.GetParticlesKey(), particlesGO);
            particlesGO = null;
        }
    }

    // @@@@@@@@@@@@@@@@@@@@@
    // POUR DEBUG

    //private bool hasCollidedOnce = false;

    //@@@@@@@@@@@@@@@@@@@@@@

    /*private void OnCollisionEnter(Collision collision)
    {
        if ((collidedElement = collision.transform.GetComponent<Element>()) != null && !hasCollidedOnce)
        {
            rotation = Quaternion.FromToRotation(Vector3.forward, transform.forward + collidedElement.transform.forward);

            if (collidedElement.GetPriority() > GetPriority()) collidedElement.GetElementData().Merge(elementData, collision.contacts[0].point, rotation);
            else elementData.Merge(collidedElement.GetElementData(), collision.contacts[0].point, rotation);

            hasCollidedOnce = true;

            elementData.Remove();
            collidedElement.GetElementData().Remove();
        }
    }*/

    public ElementData GetElementData() 
    {
        return elementData;
    }

    public void SetElementData(ElementData elementData)
    {
        this.elementData = elementData;
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
