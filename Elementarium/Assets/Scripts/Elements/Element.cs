using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour
{

    [SerializeField] protected ElementData elementData;
    private AudioSource source;
    protected GameObject particlesGO;
    protected ParticleSystem particles;
    protected Element collidedElement;
    protected List<ParticleCollisionEvent> particlesCollisions;
    protected Quaternion rotation;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

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
                particles.Play();
            }
        }
        
        PlaySound();
    }

    public void PlayParticles(Transform t, Quaternion quaternion)
    {
        particlesGO = Pooler.instance.Pop(elementData.GetParticlesKey(), t.position, quaternion);
        if (elementData.isParticuleSystem)
        {
            if ((particles = particlesGO.GetComponent<ParticleSystem>()) != null)
            {
                particles.Play();
            }
        }
        
        PlaySound();
    }

    public void PlayParticles(Transform t, Transform parent)
    {
        if (elementData.isParticuleSystem)
        {
            particlesGO = Pooler.instance.Pop(elementData.GetParticlesKey(), t.position, parent);
            if ((particles = particlesGO.GetComponent<ParticleSystem>()) != null)
            {
                particles.Play();
            }
        }
        else
        {
            particlesGO = Pooler.instance.Pop(elementData.GetParticlesKey(), t.position + t.forward/5, parent);
        }
        
        PlaySound();
    }

    public void PlayParticles(Transform t, Quaternion quaternion, Transform parent)
    {
        particlesGO = Pooler.instance.Pop(elementData.GetParticlesKey(), t.position, quaternion,  parent);
        if (elementData.isParticuleSystem)
        {
            if ((particles = particlesGO.GetComponent<ParticleSystem>()) != null)
            {
                particles.Play();
            }
        }
        
        PlaySound();
    }

    public void StopParticles()
    {
        if (particles != null && !particles.IsAlive()) 
        {
            StopPlaySound();
            particles.Stop(); 
        }

        if (particlesGO != null)
        {
            StopPlaySound();
            Pooler.instance.DePop(elementData.GetParticlesKey(), particlesGO);
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
                StopPlaySound();
                particles.Stop();
            }
        }

        if (particlesGO != null)
        {
            StopPlaySound();
            Pooler.instance.DePop(elementData.GetParticlesKey(), particlesGO);
            particlesGO = null;
        }
    }

    private void PlaySound()
    {
        if (particlesGO.GetComponent<Element>().source!=null && !particlesGO.GetComponent<Element>().source.isPlaying)
        {
            Debug.Log("Caca");
            particlesGO.GetComponent<Element>().source.clip = elementData.GetAudioClip();
            particlesGO.GetComponent<Element>().source.Play();
        }
    }

    private void StopPlaySound()
    {
        if (particlesGO.GetComponent<Element>().source != null)
        {
            particlesGO.GetComponent<Element>().source.Stop();
            particlesGO.GetComponent<Element>().source.clip = null;
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
