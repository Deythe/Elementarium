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

    public void Update()
    {
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

}
