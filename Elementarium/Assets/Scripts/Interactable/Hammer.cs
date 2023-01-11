using UnityEngine;

public class Hammer : Interactible
{
    [SerializeField] private bool usingGravity;
    [SerializeField] private Rigidbody hammer_rb, pales_rb;
    [SerializeField] private float upAccelerationForce;
    [SerializeField] private Vector3 angularAcceleration, vectorForceEnclume;
     
    protected override void Collide(Transform e)
    {
        if (e.GetComponentInParent<Element>().GetID() == ElementData.ID.AIR)
        {
            pales_rb.angularVelocity += angularAcceleration;
            hammer_rb.AddForce(vectorForceEnclume * upAccelerationForce, ForceMode.Acceleration);
        }
    }

    private void FixedUpdate()
    {
        if (!usingGravity)
        {
            hammer_rb.AddForce(-vectorForceEnclume * upAccelerationForce, ForceMode.Acceleration);
        }
    }
}
