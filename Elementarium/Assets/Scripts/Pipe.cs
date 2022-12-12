using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : Interactible
{
    [SerializeField] private List<Transform> listLinkedPiped = new List<Transform>(), listHole;
    [SerializeField] private bool isTheFirst, isTheEnd, isFeed;
    [SerializeField] private Element currentElement;
    private WaitForSeconds stopTime = new WaitForSeconds(0.5f);
    private Transform holeMoreDistant;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Pipe>() != null && other.gameObject != gameObject)
        {
            listLinkedPiped.Add(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Pipe>() != null && other.gameObject != gameObject)
        {
            listLinkedPiped.Remove(other.transform);
        }
    }
    
    protected override void Collide(Transform e)
    {
        currentElement.SetElementData(e.GetComponent<Element>().GetElementData());
        
        if (!isFeed)
        {
            isFeed = true;
            holeMoreDistant = listHole[0];
            
            foreach (var hole in listHole)
            {
                if (Vector3.Distance(hole.position, e.position) > Vector3.Distance(holeMoreDistant.position, e.position))
                {
                    holeMoreDistant = hole;
                }
            }
            
            currentElement.PlayParticles(holeMoreDistant, holeMoreDistant.rotation, transform);
            StartCoroutine(CoroutineStopWata());
        }
    }

    IEnumerator CoroutineStopWata()
    {
        yield return stopTime;
        currentElement.StopParticles();
        isFeed = false;
    }
}
