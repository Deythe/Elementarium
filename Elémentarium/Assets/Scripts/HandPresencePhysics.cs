using UnityEngine;

public class HandPresencePhysics : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Rigidbody rb;
    private Quaternion rotationDiference;
    private Vector3 rotationDiferenceInDegree;
    private void Start()
    {
        rb.maxAngularVelocity = 10000;
    }
    
    private void FixedUpdate()
    {
        rb.velocity = (target.position - transform.position) / Time.deltaTime;

        rotationDiference = target.rotation * Quaternion.Inverse(transform.rotation);
        rotationDiference.ToAngleAxis(out float angleInDegree, out Vector3 rotationAxis);
        rotationDiferenceInDegree = angleInDegree * rotationAxis;

        rb.angularVelocity = (rotationDiferenceInDegree * Mathf.Deg2Rad / Time.fixedDeltaTime);
    }
}
