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
    [SerializeField] private Transform spawnPoint; 
    [SerializeField] ParticleSystem inkParticle;
    [SerializeField] Transform parentController;
    [SerializeField] Transform splatGunNozzle;

    private bool isShooting = false;

    public void Update()
    {
        Vector3 angle = parentController.localEulerAngles;
        
        if (triggerAction.action.ReadValue<float>() > 0.1f)
        {
            inkParticle.Play();
            isShooting = true;
        }
        else if (isShooting)
        {
            inkParticle.Stop();
            isShooting = false;
        }
    }
    
}
