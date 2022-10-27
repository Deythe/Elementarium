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
    [SerializeField] private GameObject absorbGroup;
    [SerializeField] private Transform anchorTransform;
    [SerializeField] private float rayDistanceMax;
    [SerializeField] private LayerMask _layerMask;

    private bool isTouching, haveInHand = false;
    private Transform absorbedObject;
    private float whereObjectWasTouched;
    private RaycastHit hit;
    void Update()
    {
        if (gripAction.action.ReadValue<float>() > 0.5f)
        {
            absorbGroup.SetActive(true);
            
            if (!haveInHand)
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
    }

    void CheckAbsorbeObject()
    {
        isTouching = Physics.Raycast(anchorTransform.position, anchorTransform.forward, out hit, rayDistanceMax, _layerMask);

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
            whereObjectWasTouched = Mathf.Abs(Vector3.Distance(absorbedObject.position, hit.point));
        }

        absorbedObject.DOMove(
                new Vector3(anchorTransform.position.x, anchorTransform.position.y,
                    anchorTransform.position.z),
                PlayerManager.instance.p_data.timeToAbsorbObjectComeToUs)
            .OnComplete(() => MakeInHand(absorbedObject));
    }

    void FreeObject()
    {
        absorbedObject.DOKill();
        absorbedObject.GetComponent<Rigidbody>().isKinematic = false;
        absorbedObject.SetParent(null);
        absorbedObject = null;
        haveInHand = false;
    }

    void MakeInHand(Transform child)
    {
        child.SetParent(anchorTransform);
        haveInHand = true;
    }
}
