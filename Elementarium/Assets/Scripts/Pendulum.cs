using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : Interactible
{
    [SerializeField] private Transform pivot;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float windMultiplier;
    [SerializeField] private float gravityMultiplier;
    private Vector3 direction;

    private Vector3 aAcceleration;

    protected override void Collide(Transform collid)
    {
        direction = (transform.position - collid.position).normalized;
        rb.angularVelocity += new Vector3(/*-direction.z * windMultiplier*/0, 0, direction.x * windMultiplier);
    }

    private void FixedUpdate()
    {
        //rb.angularVelocity = new Vector3(rb.angularVelocity.x, 0, rb.angularVelocity.z);
        //rb.angularVelocity += new Vector3(((transform.parent.rotation.eulerAngles.z + 180)%360-180) * gravityMultiplier, 0, -((transform.parent.rotation.eulerAngles.x+180)%360-180) * gravityMultiplier);

        Debug.Log((pivot.position.x - transform.position.x));

        aAcceleration = new Vector3(0, 0, Mathf.Sin(pivot.rotation.eulerAngles.x * Mathf.Deg2Rad) * Physics.gravity.y / Mathf.Abs(pivot.position.x - transform.position.x)) * gravityMultiplier;
        rb.angularVelocity += aAcceleration;
    }
}
