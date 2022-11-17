using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Absorb : MonoBehaviour
{
    [SerializeField] private HandController masterHand;

    [SerializeField] private GameObject absorbShape;
    [SerializeField] private Transform absorbAnchorTransform;
    [SerializeField] private float rayDistanceMax, speedRotation, radiusRotation;
    [SerializeField] private LayerMask _layerMask;
    private Coroutine currentCoroutine;
    private bool isTouching;
    private Transform absorbedObject;
    private float angle;
    private RaycastHit hit;
    
    void Update()
    {
        if (masterHand.gripAction.action.ReadValue<float>() > 0.5f && masterHand.triggerAction.action.ReadValue<float>()<0.1f)
        {
            if (!masterHand.haveObjectInHand)
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
    }

    void CheckAbsorbeObject()
    {
        isTouching = Physics.Raycast(absorbAnchorTransform.position, absorbAnchorTransform.forward, out hit, rayDistanceMax, _layerMask);

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
            masterHand.haveObjectInHand = false;
        }
    }

    IEnumerator CoroutineMoveAround()
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

        masterHand.haveObjectInHand = true;
    }
}
