using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoOverlapCollider : MonoBehaviour
{
    [SerializeField] private Collider thisCollider;
    [SerializeField] private LayerMask _layerMask;
    private List<Collider> colliders = new List<Collider>();
    private float distance;
    private Vector3 direction;

    private void Start()
    {
        if (thisCollider == null)
        {
            thisCollider = GetComponent<Collider>();
        }
    }

    private void FixedUpdate()
    {
        CheckInACollider();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!colliders.Contains(collision.collider) && !collision.transform.CompareTag("Player"))// && ((1<<collision.gameObject.layer) & _layerMask) == 0)
        {
            colliders.Add(collision.collider);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (!colliders.Contains(other.collider))
        {
            colliders.Remove(other.collider);
        }
    }

    void CheckInACollider()
    {
        for (int i = 0; i < colliders.Count; i++)
        {
            if (Physics.ComputePenetration(thisCollider, transform.position, transform.rotation, colliders[i], colliders[i].transform.position, colliders[i].transform.rotation, out direction, out distance))
            {
                transform.position += direction * distance;
            }
        }
    }
}
