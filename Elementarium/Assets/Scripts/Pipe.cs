using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : Interactible
{
    [SerializeField] private List<Transform> listHole;
    [SerializeField] private bool isTheFirst, isTheEnd, isFeed;
    [SerializeField] private Element currentElement;
    [SerializeField] private List<Pipe> lastPipes = new List<Pipe>();
    [SerializeField] private List<Transform> listLinkedPiped = new List<Transform>();
    [SerializeField] private bool _isInHand;
    [SerializeField] private Rigidbody rb;
    
    private WaitForSeconds stopTime = new WaitForSeconds(0.5f);
    private Transform holeMoreDistant;
    
    public bool isOnHook
    {
        get => _isInHand;
        set
        {
            _isInHand = value;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Pipe>() != null && !listLinkedPiped.Contains(other.transform))
        {
            listLinkedPiped.Add(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Pipe>() != null && listLinkedPiped.Contains(other.transform))
        {
            listLinkedPiped.Remove(other.transform);
        }
    }

    private void LastPipes(Pipe pipe, List<Pipe> pipesCurrents)
    {
        pipesCurrents.Add(pipe);
        for (int i = 0; i < pipe.listLinkedPiped.Count; i++)
        {
            if (pipe.listLinkedPiped.Count.Equals(1) &&
                pipesCurrents.Contains(pipe.listLinkedPiped[0].GetComponent<Pipe>()))
            {
                lastPipes.Add(pipe);
                return;
            }
            
            if (!pipesCurrents.Contains(pipe.listLinkedPiped[i].GetComponent<Pipe>()))
            {
                LastPipes(pipe.listLinkedPiped[i].GetComponent<Pipe>(), pipesCurrents);
            }
        }
    }
    
    protected override void Collide(Transform e)
    {
        currentElement.SetElementData(e.GetComponent<Element>().GetElementData());
        
        if (!isFeed)
        {
            isFeed = true;
            holeMoreDistant = listHole[0];
            lastPipes.Clear();
            
            if (listLinkedPiped.Count > 0)
            {
                LastPipes(this, new List<Pipe>());
            }
            else
            {
                lastPipes.Add(this);
            }

            
            foreach (var pipe in lastPipes)
            {
                foreach (var hole in pipe.listHole)
                {
                    if (Vector3.Distance(hole.position, e.position) > Vector3.Distance(holeMoreDistant.position, e.position))
                    {
                        holeMoreDistant = hole;
                    }
                }
                currentElement.PlayParticles(holeMoreDistant, holeMoreDistant.rotation, transform);
            }
            
            StartCoroutine(CoroutineStopWata());
        }
    }

    public void Hook()
    {
        if (isOnHook)
        {
            //rb.fre
        }
    }

    public void UnHook()
    {
        if (isOnHook)
        {
            
        }
    }

    IEnumerator CoroutineStopWata()
    {
        yield return stopTime;
        currentElement.StopParticles();
        isFeed = false;
    }
}
