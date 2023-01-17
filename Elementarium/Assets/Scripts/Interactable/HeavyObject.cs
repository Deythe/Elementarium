using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
            Debug.Log(hit.transform.name);

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
        xDiff = e.position.x - transform.position.x;
        zDiff = e.position.z - transform.position.z;

        if (Mathf.Abs(xDiff) > Mathf.Abs(zDiff))
        {
            rb.velocity = new Vector3((xDiff < 0 ? 1 : -1) * velocity, 0, 0);
            //rb.AddForce((xDiff < 0 ? 1 : -1) * velocity, 0, 0);
        }
        else
        {
            rb.velocity = new Vector3(0, 0, (zDiff < 0 ? 1 : -1) * velocity);
            //rb.AddForce(0, 0, (zDiff < 0 ? 1 : -1) * velocity);
        }
    }

    private void ResetVelocity()
    {
        rb.velocity = Vector3.zero;
    }
}
