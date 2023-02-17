using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Glass : MonoBehaviour, IDestroyable
{
    //[SerializeField] private GameObject originalObject;
    [SerializeField] private MeshRenderer originalMeshRenderer;
    [SerializeField] private Collider originalCollider;
    [SerializeField] private GameObject fracturedObject;
    [SerializeField] private float impactSpeedMin;
    [SerializeField] private float impactForceMultiplier;
    [SerializeField] private float explosionForceRadius;
    [SerializeField] private UnityEvent actionWhenBroke;
    
    private Rigidbody rbFractured;
    private Vector3 impactPosition;
    private float impactPositionOffsetMultiplier = -0.5f;
    private float impactRelativeSpeed;
    //[SerializeField] private LayerMask destroyerLayerMask;

    [ContextMenu("Break Glass")]
    public void DestroyObject()
    {
        actionWhenBroke.Invoke();

        originalMeshRenderer.enabled = false;
        originalCollider.enabled = false;
        fracturedObject.SetActive(true);
        foreach (Transform t in fracturedObject.transform) 
        {
            rbFractured = t.GetComponent<Rigidbody>();
            if (rbFractured != null) 
            {
                rbFractured.AddExplosionForce(impactRelativeSpeed * impactForceMultiplier, impactPosition, explosionForceRadius);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((impactRelativeSpeed = collision.relativeVelocity.magnitude) > impactSpeedMin && collision.transform.GetComponent<IDestroyer>() != null)
        {
            impactPosition = collision.GetContact(0).point +  new Vector3(collision.GetContact(0).normal.x * impactPositionOffsetMultiplier, collision.GetContact(0).normal.y * impactPositionOffsetMultiplier, collision.GetContact(0).normal.z * impactPositionOffsetMultiplier);
            DestroyObject();
        }
    }
}
