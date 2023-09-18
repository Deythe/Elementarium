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

    private float armLength;
    private Vector3 aAcceleration;

    private void Start()
    {
        armLength = (pivot.position - transform.position).magnitude;
    }

    protected override void Collide(Transform collid)
    {
        direction = (transform.position - collid.position).normalized;
        rb.angularVelocity += new Vector3(-direction.z * windMultiplier, 0, direction.x * windMultiplier);
    }

    private void FixedUpdate()
    {
        //rb.angularVelocity = new Vector3(rb.angularVelocity.x, 0, rb.angularVelocity.z);
        //rb.angularVelocity += new Vector3(((transform.parent.rotation.eulerAngles.z + 180)%360-180) * gravityMultiplier, 0, -((transform.parent.rotation.eulerAngles.x+180)%360-180) * gravityMultiplier);



        aAcceleration = new Vector3(-(gravityMultiplier / armLength) * Mathf.Sin(pivot.rotation.eulerAngles.z * Mathf.Deg2Rad), (gravityMultiplier / armLength) * Mathf.Sin(pivot.rotation.eulerAngles.y * Mathf.Deg2Rad), (gravityMultiplier / armLength) * Mathf.Sin(pivot.rotation.eulerAngles.x * Mathf.Deg2Rad)); //* gravityMultiplier;
        rb.angularVelocity += aAcceleration;
        Debug.Log((gravityMultiplier/ armLength) + "\n" +
            pivot.rotation.eulerAngles.x * Mathf.Deg2Rad + "\n" +
            Mathf.Sin(pivot.rotation.eulerAngles.x * Mathf.Deg2Rad) + "\n" +
            (gravityMultiplier / armLength) * Mathf.Sin(pivot.rotation.eulerAngles.x * Mathf.Deg2Rad));
        Debug.Log("ANGULAR ACCELERATION" + aAcceleration.z);
    }
}
