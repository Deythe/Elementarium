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
        
        if (triggerAction.action.WasPerformedThisFrame())
        {
            VisualPolish();
            inkParticle.Play();
        }
        else if (triggerAction.action.WasReleasedThisFrame())
        {
            inkParticle.Stop();
        }
    }
    
    void VisualPolish()
    {
        if (!DOTween.IsTweening(parentController))
        {
            parentController.DOComplete();
            Vector3 forward = -parentController.forward;
            Vector3 localPos = parentController.localPosition;
            parentController.DOLocalMove(localPos - new Vector3(0, 0, .2f), .03f)
                .OnComplete(() => parentController.DOLocalMove(localPos, .1f).SetEase(Ease.OutSine));

        }
    }
    
}
