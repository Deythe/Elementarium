using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using UnityEngine.Serialization;
using UnityEngine.XR;


public class Expulse : MonoBehaviour
{
    [SerializeField] private HandController masterHand;
    [SerializeField] ParticleSystem inkParticle;
    [SerializeField] Transform parentController;
    [SerializeField] Transform splatGunNozzle;

    [SerializeField] private Element element;

    [SerializeField] [Range(0, 1)] private float cooldownMin, cooldownMax;
    
    private float cooldown;
    private GameObject bullet;

    private void Start()
    {
        cooldown = UnityEngine.Random.Range(cooldownMin, cooldownMax);
    }

    public void Update()
    {
        Vector3 angle = parentController.localEulerAngles;
        
        if (masterHand.triggerAction.action.ReadValue<float>() > 0.5f && masterHand.gripAction.action.ReadValue<float>()<0.1f)
        {
            elemParticle.gameObject.SetActive(true);
            elemParticle.Play();
        }
        else
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
