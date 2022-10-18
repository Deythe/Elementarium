using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    [SerializeField]
    private GameObject metaballParentPrefab;
    [SerializeField] private GameObject metaballPrefab;
    private GameObject metaballPool;
    private GameObject metaball;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            metaballPool = Instantiate(metaballParentPrefab);
            metaballPool.transform.position = transform.position;
            Destroy(gameObject);
        }
        else if (other.CompareTag("Metaball"))
        {
            if (other.transform.GetComponent<MCBlob>().blobs.Length < 20)
            {
                Debug.Log(other.transform.GetComponent<MCBlob>().blobs.Length);
                metaball = Instantiate(metaballPrefab);
                metaball.transform.position = transform.position;
                metaball.transform.parent = other.transform;
                //metaball.transform.localScale = Vector3.one;
                other.transform.GetComponent<MCBlob>().AddBlobs();
            }
            
            Destroy(gameObject);
        }
    }
}
