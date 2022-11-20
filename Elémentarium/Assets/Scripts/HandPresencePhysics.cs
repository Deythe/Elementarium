using Mono.Cecil;
using UnityEngine;

public class HandPresencePhysics : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Rigidbody rb;
    private Quaternion rotationDiference;
    private Vector3 rotationDiferenceInDegree;

    public Transform target
    {
        get => _target;
    }
    
    private void Start()
    {
        rb.maxAngularVelocity = 10000;
    }
    
    private void FixedUpdate()
    {
        rb.velocity = (_target.position - transform.position) / Time.deltaTime;

        rotationDiference = _target.rotation * Quaternion.Inverse(transform.rotation);
        rotationDiference.ToAngleAxis(out float angleInDegree, out Vector3 rotationAxis);
        rotationDiferenceInDegree = angleInDegree * rotationAxis;

        rb.angularVelocity = (rotationDiferenceInDegree * Mathf.Deg2Rad / Time.fixedDeltaTime);
    }
}
