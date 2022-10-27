using System.Collections;
using System.Collections.Generic;
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
    
    private Transform absorbedObject;
    private float whereObjectWasTouched, timerForAbsorbed;
    private RaycastHit hit;
    void Update()
    {
        if (gripAction.action.ReadValue<float>() > 0.5f)
        {
            absorbGroup.SetActive(true);
            
            if (absorbedObject==null)
            {
                if (Physics.Raycast(anchorTransform.position, anchorTransform.forward, out hit, rayDistanceMax, _layerMask))
                {
                    absorbedObject = hit.transform;
                    absorbedObject.GetComponent<Rigidbody>().isKinematic = true;
                    whereObjectWasTouched = Mathf.Abs(Vector3.Distance(absorbedObject.position, hit.point));
                    absorbedObject.position = new Vector3(anchorTransform.position.x, anchorTransform.position.y, anchorTransform.position.z + whereObjectWasTouched);
                    absorbedObject.SetParent(anchorTransform);
                }
            }
        }
        else
        {
            if (absorbedObject != null)
            {
                absorbedObject.GetComponent<Rigidbody>().isKinematic = false;
                absorbedObject.SetParent(null);
                absorbedObject = null;
            }
            
            absorbGroup.SetActive(false);
        }
    }

    IEnumerator CoroutineAbsorbedObject()
    {
        yield return new WaitForSeconds(0);
    }
}
