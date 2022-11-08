using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using UnityEngine.XR;


public class Expulse : MonoBehaviour
{
    [SerializeField] private InputActionProperty triggerAction;
    [SerializeField] ParticleSystem elemParticle;

    private ParticleSystem currentElement;
    
    
    public void Update()
    {
        //GetElement();
        //if (currentElement == null) return;
        if (triggerAction.action.WasPerformedThisFrame())
        {
            elemParticle.gameObject.SetActive(true);
            elemParticle.Play();
        }
        else if (triggerAction.action.WasReleasedThisFrame())
        {
            elemParticle.Stop();
            elemParticle.gameObject.SetActive(false);
        }
    }

    private void GetElement()
    {
        throw new NotImplementedException();
    }
}
