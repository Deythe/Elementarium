using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glass : MonoBehaviour, IDestroyable
{
    //[SerializeField] private GameObject originalObject;
    [SerializeField] private MeshRenderer originalMeshRenderer;
    [SerializeField] private Collider originalCollider;
    [SerializeField] private GameObject fracturedObject;
    private Rigidbody rbFractured;
    [SerializeField] private float impactSpeedMin;
    [SerializeField] private float impactForceMultiplier;
    private Vector3 impactPosition;
    private float impactPositionOffsetMultiplier = -0.5f;
    private float impactRelativeSpeed;
    [SerializeField] private float explosionForceRadius;
    //[SerializeField] private LayerMask destroyerLayerMask;

    public void DestroyObject()
    {
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
        Debug.Log("Collision entered");
        if ((impactRelativeSpeed = collision.relativeVelocity.magnitude) > impactSpeedMin && collision.transform.GetComponent<IDestroyer>() != null)
        {
            Debug.Log("Collision Condition entered");
            impactPosition = collision.GetContact(0).point +  new Vector3(collision.GetContact(0).normal.x * impactPositionOffsetMultiplier, collision.GetContact(0).normal.y * impactPositionOffsetMultiplier, collision.GetContact(0).normal.z * impactPositionOffsetMultiplier);
            DestroyObject();
        }
    }
}
