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
    [SerializeField] private HandController motherHand;
    [SerializeField] private float sourceRange;
    [SerializeField] private LayerMask layerMask;

    private GameObject elementGO;
    private ParticleSystem elementPS;
    private Transform parentController;
    private RaycastHit hit;
    private bool hasShot;
    
    
    private void Start()
    {
        parentController = motherHand.transform;
        //motherHand = GetComponent<Element>();
    }
    
    public void Update()
    {
        CheckForSources();
        FireElement();
    }

    private void FireElement()
    {
        if (motherHand.element == null) return;
        
        if (motherHand.triggerAction.action.ReadValue<float>() > 0.5f && motherHand.gripAction.action.ReadValue<float>()<0.1f)
        {
            if (!hasShot)
            {
                motherHand.element.PlayParticles(transform, transform);
                hasShot = true;
            }
        }
        else
        {
            motherHand.element.StopParticles();
            hasShot = false;
        }
    }

    private void CheckForSources()
    {
        if (Physics.Raycast(transform.position + transform.forward /10, transform.forward,  out hit, sourceRange,  layerMask))
        {
            if (motherHand.gripAction.action.ReadValue<float>() > 0.5f &&
                motherHand.triggerAction.action.ReadValue<float>() < 0.1f)
            {
                motherHand.element.SetElementData(hit.collider.GetComponent<Element>().GetElementData());
            }
        }
    }

}
