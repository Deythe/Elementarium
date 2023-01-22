using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;

public class HeavyObject : Interactible
{
    [SerializeField] private ElementData.ID slidingElement;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private float velocity;

    private RaycastHit hit;
    [SerializeField] private float offsetOrigin;
    [SerializeField] private float raycastDistance;
    private Element element;
    private float xDiff;
    private float zDiff;
    private bool canMove = true;

    private void Update()
    {
        Debug.DrawRay(transform.position + Vector3.down * offsetOrigin, Vector3.down * raycastDistance, Color.blue);
        if (rb.velocity.sqrMagnitude < (velocity * velocity) / 2) 
        {
            ResetVelocity();
            canMove = true;
        }
    }

    protected override void Collide(Transform e)
    {

        if (Physics.Raycast(transform.position + Vector3.down * offsetOrigin, Vector3.down, out hit, raycastDistance))
        {
            element = hit.transform.GetComponent<Element>();

            if (element == null || element.GetID() != slidingElement)
            {
                ResetVelocity();
            }
            else if(canMove)
            {
                canMove = false;
                CalculateVelocity(e);
            }
        }
    }

    private void CalculateVelocity(Transform e)
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 toOther = transform.position - e.position;
        
        float angle = (int)(Vector3.Dot(forward, toOther));
        //Debug.Log(angle);
        if (angle != 0)
        {
            rb.velocity = angle > 0 ? transform.forward * velocity : -transform.forward * velocity;
            
        }
        else
        {
            Vector3 cross = Vector3.Cross(Vector3.forward, toOther);
            float sideAngle = (int)(Vector3.Dot(toOther, transform.right));
            Debug.Log(sideAngle);
            if (sideAngle < 0)
            {
                rb.velocity = -transform.right * velocity;
                //Debug.Log("Left");
            }
            else 
            {
                rb.velocity = transform.right* velocity;
                //Debug.Log("Right");
            }
            //rb.velocity = e.transform.position.x > transform.position.x ? -transform.right * velocity : ;
        }
    }

    private void ResetVelocity()
    {
        rb.velocity = Vector3.zero;
    }
}
