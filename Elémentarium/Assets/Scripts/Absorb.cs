using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit;

public class Absorb : MonoBehaviour
{
    [SerializeField] private HandController masterHand;
    
    [SerializeField] private GameObject absorbShape, absorbGroup;
    [SerializeField] private Transform absorbAnchorTransform, inHandAnchorTranform;
    [SerializeField] private float rayDistanceMax, speedRotation, radiusRotation;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float multiplicator;
    [SerializeField] private Animator _animator;
    
    private Coroutine currentCoroutine;
    private Vector3 distanceBetweenPivot;
    private bool isTouching;
    private Transform absorbedObject;
    private float angle;
    private RaycastHit hit;
    
    void Update()
    {
        if (masterHand.gripAction.action.ReadValue<float>() > 0.5f && masterHand.triggerAction.action.ReadValue<float>()<0.1f)
        {
            if (!masterHand.haveInHand)
            {
                CheckAbsorbeObject();
            }
        }
        else
        {
            if (absorbedObject != null)
            {
                FreeObject();
            }
            
            absorbGroup.SetActive(false);
        }


        _animator.SetBool("hasAnObject", masterHand.haveInHand);
    }

    void CheckAbsorbeObject()
    {
        isTouching = Physics.Raycast(absorbAnchorTransform.position, absorbAnchorTransform.forward, out hit, rayDistanceMax, _layerMask);

        if (!isTouching)
        {
            if (absorbedObject != null)
            {
                FreeObject();
                return;
            }
            
            return;
        }
        
        if (hit.transform!=absorbedObject)
        {
            FreeObject();
            absorbedObject = hit.transform;
            absorbedObject.GetComponent<Rigidbody>().isKinematic = true;
            //whereObjectWasTouched = Mathf.Abs(Vector3.Distance(absorbedObject.position, hit.point));
        }

        absorbedObject.DOMove(
                new Vector3(absorbAnchorTransform.position.x, absorbAnchorTransform.position.y,
                    absorbAnchorTransform.position.z),
                PlayerManager.instance.p_data.timeToAbsorbObjectComeToUs)
            .OnComplete(() => MakeInHand(absorbedObject));
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
            absorbedObject.GetComponent<Interactable>().CheckInACollider();
            absorbedObject = null;
            masterHand.haveInHand = false;
        }
    }

    IEnumerator MakeInHand(Transform child)
    {
        angle = 0;
        while (Mathf.Abs(Vector3.Distance(absorbedObject.position, absorbAnchorTransform.position)) > 0.1f)
        {
            absorbedObject.localPosition = new Vector3(Mathf.Cos(angle)/(radiusRotation+angle), Mathf.Sin(angle)/(radiusRotation+angle), absorbedObject.localPosition.z-0.01f);
            
            if (absorbedObject.localPosition.z <= absorbAnchorTransform.localPosition.z)
            {
                absorbedObject.localPosition = new Vector3(absorbedObject.localPosition.x,
                    absorbedObject.localPosition.y, absorbAnchorTransform.localPosition.z);
            }
            
            angle += Time.deltaTime * speedRotation;
            yield return new WaitForFixedUpdate();
        }

        masterHand.haveInHand = true;
        
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
