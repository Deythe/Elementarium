using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Absorb : MonoBehaviour
{
    [SerializeField] private HandController masterHand;
    [SerializeField] private GameObject absorbShape;
    [SerializeField] private Transform absorbAnchorTransform, inHandAnchor;
    [SerializeField] private float rayDistanceMax, speedRotation, radiusRotation;
    [SerializeField] private LayerMask _layerMask;
    
    
    private Coroutine currentCoroutine;
    private bool isTouching, isAbsorbing;
    private Transform absorbedObject;
    private float angle, distance, maxDistance;
    private RaycastHit hit;
    
    void Update()
    {
        if (masterHand.haveObjectInHand) return;
        
        if (masterHand.gripAction.action.ReadValue<float>() > 0.95f &&
            masterHand.triggerAction.action.ReadValue<float>() > 0.95f)
        {
            isAbsorbing = true;
            absorbShape.SetActive(true);
            CheckAbsorbedObject();
        }
        else
        {
            CancelAbsorb();
            isAbsorbing = false;
            absorbShape.SetActive(false);
        }
    }

    void CheckAbsorbedObject()
    {
        if (absorbedObject == null)
        {
            isTouching = Physics.SphereCast(absorbAnchorTransform.position, 0.3f, absorbAnchorTransform.forward, out hit,
                rayDistanceMax);
        }
        
        if (hit.transform == null) return;
        if (hit.transform == absorbedObject) return;
        if (((1<<hit.transform.gameObject.layer) & _layerMask) == 0) return;
        
        if (hit.transform.gameObject.layer.Equals(12)) 
        {
            masterHand.element.SetElementData(hit.collider.GetComponent<Element>().GetElementData());
            return;
        }

        CancelAbsorb();
        absorbedObject = hit.transform;
        absorbedObject.SetParent(absorbAnchorTransform);
        absorbedObject.GetComponent<Rigidbody>().isKinematic = true;
        currentCoroutine = StartCoroutine(CoroutineMoveAround());
    }

    void CancelAbsorb()
    {
        if (absorbedObject != null)
        {
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }
            
            absorbedObject.GetComponent<Rigidbody>().isKinematic = false;
            absorbedObject.SetParent(null);
            absorbedObject = null;
        }
        
        masterHand.haveObjectInHand = false;
    }

    public void Release()
    {
        masterHand.haveObjectInHand = false;
    }

    public void Grabbed()
    {
        masterHand.haveObjectInHand = true;

        if (isAbsorbing)
        {
            StopCoroutine(currentCoroutine);
            absorbedObject.GetComponent<Rigidbody>().isKinematic = false;
            absorbedObject.SetParent(null);
            absorbShape.SetActive(false);
        }
    }

    IEnumerator CoroutineMoveAround()
    {
        angle = 0;
        maxDistance = Mathf.Abs(Vector3.Distance(absorbedObject.position, absorbAnchorTransform.position)); 
        do
        {
            absorbedObject.LookAt(absorbAnchorTransform);
            distance = Mathf.Abs(Vector3.Distance(absorbedObject.position, absorbAnchorTransform.position)); 
            absorbedObject.localPosition = new Vector3(Mathf.Cos(angle)*distance*0.5f/maxDistance, Mathf.Sin(angle)*distance*0.5f/maxDistance, absorbedObject.localPosition.z-0.01f);
            angle += Time.deltaTime * speedRotation;
            yield return new WaitForFixedUpdate();
        }
        while (!masterHand.haveObjectInHand) ;
    }
}
