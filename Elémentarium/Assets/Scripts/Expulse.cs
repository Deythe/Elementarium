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
    [SerializeField] private ElementData element;
    [SerializeField] private float sourceRange;
    [SerializeField] private LayerMask layerMask;

    private GameObject elementGO;
    private Element currentElement;
    private ParticleSystem elementPS;
    private Transform parentController;
    private RaycastHit hit;
    private bool hasElement;

    //[SerializeField] private Element element;
    
    private void Start()
    {
        parentController = playerHand.transform;
    }

    private void PopulateElement()
    {
        hasElement = true;
        elementGO = Pooler.instance.Pop(element.GetParticlesKey());
        elementPS = elementGO.GetComponent<ParticleSystem>();
        elementGO.transform.parent = parentController;
        elementGO.transform.localPosition = Vector3.zero;
        elementGO.transform.localRotation = Quaternion.identity;
    }

    private void FireElement()
    {
        if (elementPS == null) return;
        Vector3 angle = parentController.localEulerAngles;
        
        if (playerHand.triggerAction.action.ReadValue<float>() > 0.5f && playerHand.gripAction.action.ReadValue<float>()<0.1f)
        {
            currentElement.PlayParticles(parentController, parentController);
        }
        else
        {
            currentElement.StopParticles();
        }
    }

    private void CheckForSources()
    {
        if (Physics.Raycast(transform.position + transform.forward /10, transform.forward,  out hit, sourceRange,  layerMask))
        {
            if (playerHand.gripAction.action.ReadValue<float>() > 0.5f &&
                playerHand.triggerAction.action.ReadValue<float>() < 0.1f)
            {
                currentElement = hit.collider.GetComponent<Element>();
                ChangeElement(currentElement.GetElementData());
            }
        }
    }
    
    private void ChangeElement(ElementData newElement)
    {
        if (hasElement)
        {
            //Pooler.instance.DePop(element.GetParticlesKey(), elementGO);
        }
        element = newElement;
        PopulateElement();
    }


    public void Update()
    {
        CheckForSources();
        FireElement();
    }

    public void SetElement(ElementData element)
    {
        this.element = element;
    }
}
