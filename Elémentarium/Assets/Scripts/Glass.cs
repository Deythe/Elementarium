using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glass : MonoBehaviour, IDestroyable
{
    [SerializeField] private float impactSpeedMax;
    [SerializeField] private LayerMask destroyerLayerMask;

    public void DestroyObject()
    {
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.rigidbody.velocity.magnitude > impactSpeedMax && (destroyerLayerMask & (1 << collision.gameObject.layer)) != 0) 
        if (collision.relativeVelocity.magnitude > impactSpeedMax && collision.transform.GetComponent<IDestroyer>() != null)
        {
            DestroyObject();
        }
    }
}
