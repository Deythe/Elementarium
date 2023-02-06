using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Absorb : MonoBehaviour
{
    [SerializeField] private HandController masterHand;
    [SerializeField] private GameObject absorbShape;
    [SerializeField] private Transform absorbAnchorTransform;
    [SerializeField] private float rayDistanceMax;
    [SerializeField] private float absorbSpeed;
    [SerializeField] private float absorbDrag;
    [SerializeField] private float absorbCenterSpeed;
    [SerializeField] private float absorbCenterDrag;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private AudioClip absorbSound;
    [SerializeField] private Rigidbody rb;
    private GrabbableObjects grabbableObj;
    
    private Coroutine currentCoroutine;
    private bool isTouching, isAbsorbing;
    private Transform absorbedObject;
    private float angle, distance, maxDistance, dot;
    private RaycastHit hit;

    void Update()
    {
        if (masterHand.haveObjectInHand) return;
        
        if (masterHand.gripAction.action.ReadValue<float>() > 0.95f &&
            masterHand.triggerAction.action.ReadValue<float>() > 0.95f)
        {
            if (!masterHand.haveShot)
            {
                masterHand.PlaySound(absorbSound);
                isAbsorbing = true;
                absorbShape.SetActive(true);
                CheckAbsorbedObject();
            }
        }
        else
        {
            CancelAbsorb();
            isAbsorbing = false;
            absorbShape.SetActive(false);
        }
    }

    private GrabbableObjects otherGrabObj;
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
        if ((otherGrabObj = hit.transform.GetComponent<GrabbableObjects>()) != null)
        {
            if (otherGrabObj.isGrabbed)
            {
                return;
            };
        }

        if (hit.transform.gameObject.layer.Equals(12))
        {
            masterHand.element.SetElementData(hit.collider.GetComponent<Element>().GetElementData());
            masterHand.ChangeGemMesh();
            return;
        }

        CancelAbsorb();

        absorbedObject = hit.transform;
        //absorbedObject.SetParent(absorbAnchorTransform);
        rb = absorbedObject.GetComponent<Rigidbody>();
        grabbableObj = absorbedObject.GetComponent<GrabbableObjects>();
        //rb.isKinematic = true;
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        rb.drag = 1f;
        currentCoroutine = StartCoroutine(CoroutineMoveAround(hit));
    }

    void CancelAbsorb()
    {
        masterHand.StopSound();
        if (absorbedObject != null)
        {
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }

            //rb.isKinematic = false;
            rb.useGravity = true;
            //rb.drag = 0;
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
            masterHand.StopSound();
            StopCoroutine(currentCoroutine);
            rb.isKinematic = false;
            absorbedObject.SetParent(null);
            absorbShape.SetActive(false);
        }
    }

    IEnumerator CoroutineMoveAround(RaycastHit hit) 
    {
        while (!masterHand.haveObjectInHand) 
        {
            rb.velocity += (absorbAnchorTransform.position - absorbedObject.position).normalized * Time.deltaTime * absorbSpeed;
            dot = Vector3.Dot(absorbAnchorTransform.forward, (absorbedObject.position - absorbAnchorTransform.position));
            rb.velocity += ((transform.forward * dot + absorbAnchorTransform.position - absorbedObject.position) * Time.deltaTime * absorbCenterSpeed * (1 / (dot == 0 ? 0.001f : dot)));

            yield return new WaitForFixedUpdate();
            rb.velocity -= (absorbAnchorTransform.position - absorbedObject.position).normalized * Time.deltaTime * absorbSpeed * absorbDrag;// * 0.9f;
            rb.velocity -= ((transform.forward * dot + absorbAnchorTransform.position - absorbedObject.position) * Time.deltaTime * absorbCenterSpeed * (1 / (dot == 0 ? 0.001f : dot))) * absorbCenterDrag;
        }
        Release();
    }

}
