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
        if (elementData.isParticuleSystem)
        {
            if ((particles = particlesGO.GetComponent<ParticleSystem>()) != null)
            {
                //AudioManager.instance.PlayPist(elementData.GetAudioClip());
                particles.Play();
            }
        }
    }

    public void PlayParticles(Transform t, Quaternion quaternion)
    {
        particlesGO = Pooler.instance.Pop(elementData.GetParticlesKey(), t.position, quaternion);
        if (elementData.isParticuleSystem)
        {
            if ((particles = particlesGO.GetComponent<ParticleSystem>()) != null)
            {
                //AudioManager.instance.PlayPist(elementData.GetAudioClip());
                particles.Play();
            }
        }
    }

    public void PlayParticles(Transform t, Transform parent)
    {
        if (elementData.isParticuleSystem)
        {
            particlesGO = Pooler.instance.Pop(elementData.GetParticlesKey(), t.position, parent);
            if ((particles = particlesGO.GetComponent<ParticleSystem>()) != null)
            {
                //AudioManager.instance.PlayPist(elementData.GetAudioClip());
                particles.Play();
            }
        }
        else
        {
            particlesGO = Pooler.instance.Pop(elementData.GetParticlesKey(), t.position + t.forward/5, parent);
            //AudioManager.instance.PlayPist(elementData.GetAudioClip());
        }
    }

    public void PlayParticles(Transform t, Quaternion quaternion, Transform parent)
    {
        particlesGO = Pooler.instance.Pop(elementData.GetParticlesKey(), t.position, quaternion,  parent);
        if (elementData.isParticuleSystem)
        {
            if ((particles = particlesGO.GetComponent<ParticleSystem>()) != null)
            {
                //AudioManager.instance.PlayPist(elementData.GetAudioClip());
                particles.Play();
            }
        }
    }

    public void StopParticles()
    {
        if (particles != null && !particles.IsAlive()) 
        {
            particles.Stop();
            //AudioManager.instance.StopPist(elementData.GetAudioClip());
        }

        if (particlesGO != null)
        {
            Pooler.instance.DePop(elementData.GetParticlesKey(), particlesGO);
            //AudioManager.instance.StopPist(elementData.GetAudioClip());
            particlesGO = null;
        }
    }

    public void DetacheFromHand()
    {
        if (particlesGO != null)
        {
            particlesGO.GetComponent<Rigidbody>().isKinematic = false;
            particlesGO.transform.SetParent(null);
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
                //AudioManager.instance.StopPist(elementData.GetAudioClip());
                particles.Stop();
            }
        }

        if (particlesGO != null)
        {
            //AudioManager.instance.StopPist(elementData.GetAudioClip());
            Pooler.instance.DePop(elementData.GetParticlesKey(), particlesGO);
            particlesGO = null;
        }
    }

    public void DelayedDepopThis(float t) 
    {
        Pooler.instance.DelayedDePop(t, elementData.GetName(), this.gameObject);
    }

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

    public void ResetElement()
    {
        SetElementData(null);
    }
}
