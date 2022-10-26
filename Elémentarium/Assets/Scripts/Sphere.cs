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
            metaballPool = Pooler.instance.Pop("Liquid");
            metaballPool.transform.position = transform.position;
            //Pooler.instance.DePop("Water", gameObject);
        }
        else if (other.CompareTag("Metaball"))
        {
            if (other.transform.GetComponent<MCBlob>().blobs.Length < 10)
            {
                metaball = Pooler.instance.Pop("Metaball");
                metaball.transform.position = transform.position;
                metaball.transform.parent = other.transform;
                //metaball.transform.localScale = Vector3.one;
                other.transform.GetComponent<MCBlob>().AddBlobs();
            }
            gameObject.SetActive(false);
            //Pooler.instance.DePop("Water", gameObject);
        }
    }
}
