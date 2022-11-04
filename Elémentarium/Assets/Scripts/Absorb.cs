using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class Absorb : MonoBehaviour
{
    [SerializeField] private HandController hand;
    
    [SerializeField] private InputActionProperty gripAction;
    [SerializeField] private GameObject absorbShape;
    [SerializeField] private Transform absorbAnchorTransform, inHandAnchorTranform;
    [SerializeField] private float rayDistanceMax, speedRotation, radiusRotation;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float multiplicator;
    [SerializeField] private Animator _animator;
    
    private Coroutine currentCoroutine;
    private Vector3 distanceBetweenPivot;
    private bool isTouching, haveInHand = false;
    private Transform absorbedObject;
    private float angle, distance;
    private RaycastHit hit;
    
    void Update()
    {
        if (gripAction.action.ReadValue<float>() > 0.5f)
        {
            if (!haveInHand)
            {
                absorbShape.SetActive(true);
                CheckAbsorbeObject();
            }
        }
        else
        {
            FreeObject();
            absorbShape.SetActive(false);
        }


        _animator.SetBool("hasAnObject", haveInHand);
    }

    void CheckAbsorbeObject()
    {
        if (absorbedObject == null)
        {
            isTouching = Physics.SphereCast(absorbAnchorTransform.position, 0.3f, absorbAnchorTransform.forward, out hit,
                rayDistanceMax, _layerMask);
        }
        
        if (hit.transform != null)
        {
            if (hit.transform != absorbedObject)
            {

                FreeObject();
                absorbedObject = hit.transform;

                absorbedObject.SetParent(absorbAnchorTransform);
                absorbedObject.GetComponent<Rigidbody>().isKinematic = true;
                currentCoroutine = StartCoroutine(CoroutineMoveAround());
            }
            
            absorbedObject.LookAt(absorbAnchorTransform);
        }
    }

    void FreeObject()
    {
        if (absorbedObject != null)
        {
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }

            absorbedObject.DOKill();
            absorbedObject.GetComponent<Rigidbody>().isKinematic = false;
            absorbedObject.SetParent(null);
            absorbedObject = null;
            haveInHand = false;
        }
    }

    IEnumerator CoroutineMoveAround()
    {
        angle = 0;
        
        while (Vector3.Distance(absorbedObject.position, absorbAnchorTransform.position) > 0.2f)
        {
            absorbedObject.localPosition = new Vector3(Mathf.Cos(angle)/(radiusRotation+angle), Mathf.Sin(angle)/(radiusRotation+angle), absorbedObject.localPosition.z-0.01f);
            angle += Time.deltaTime * speedRotation;
            yield return new WaitForFixedUpdate();
        }

        haveInHand = true;
        
        Debug.Log(absorbedObject.name);
        if (absorbedObject.GetComponent<Interactable>().p_isGrabable)
        {
            _animator.SetTrigger("grab");
        }
        
        absorbShape.SetActive(false);
        absorbedObject.position = inHandAnchorTranform.position;
        absorbedObject.rotation = inHandAnchorTranform.rotation;
        distanceBetweenPivot = absorbedObject.position - absorbedObject.GetChild(0).position;

        absorbedObject.position += distanceBetweenPivot;
    }
}
