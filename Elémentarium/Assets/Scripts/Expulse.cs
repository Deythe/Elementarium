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
    [SerializeField] private HandController playerHand;
    [SerializeField] private float sourceRange;
    [SerializeField] private LayerMask layerMask;

    private GameObject elementGO;
    private Element element;
    private ParticleSystem elementPS;
    private Transform parentController;
    private RaycastHit hit;
    private bool hasShot;

    //[SerializeField] private Element element;
    
    private void Start()
    {
        parentController = playerHand.transform;
        element = GetComponent<Element>();
    }

    private void FireElement()
    {
        if (element == null) return;
        
        if (playerHand.triggerAction.action.ReadValue<float>() > 0.5f && playerHand.gripAction.action.ReadValue<float>()<0.1f)
        {
            if (!hasShot)
            {
                element.PlayParticles(transform, transform);
                hasShot = true;
            }
        }
        else
        {
            element.StopParticles();
            hasShot = false;
        }
    }

    private void CheckForSources()
    {
        if (Physics.Raycast(transform.position + transform.forward /10, transform.forward,  out hit, sourceRange,  layerMask))
        {
            if (playerHand.gripAction.action.ReadValue<float>() > 0.5f &&
                playerHand.triggerAction.action.ReadValue<float>() < 0.1f)
            {
                element.SetElementData(hit.collider.GetComponent<Element>().GetElementData());
            }
        }
    }


    public void Update()
    {
        CheckForSources();
        FireElement();
    }
    
}
