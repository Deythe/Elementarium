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
    private List<ParticleCollisionEvent> currentCollision = new List<ParticleCollisionEvent>();
    private WaitForSeconds stopTime = new WaitForSeconds(0.5f);
    private Transform holeMoreDistant, _hookAttached;
    
    public bool isOnHook
    {
        get => _isInHand;
        set
        {
            _isInHand = value;
        }
    }

    public Transform hookAttached
    {
        get => _hookAttached;
        set
        {
            _hookAttached = value;
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
        if (!isFeed)
        {
            e.GetComponent<ParticleSystem>().GetCollisionEvents(gameObject, currentCollision);
            foreach (var part in currentCollision)
            {
                foreach (var localhole in listHole)
                {
                    if (Vector3.Distance(localhole.position, part.intersection) <= 0.2f)
                    {
                        currentElement.SetElementData(e.GetComponent<Element>().GetElementData());

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
                                if (Vector3.Distance(hole.position, part.intersection) >
                                    Vector3.Distance(holeMoreDistant.position, part.intersection))
                                {
                                    holeMoreDistant = hole;
                                }
                            }

                            currentElement.PlayParticles(holeMoreDistant, holeMoreDistant.rotation, transform);
                        }

                        StartCoroutine(CoroutineStopWata());
                        return;
                    }
                }
            }
        }
    }

    public void Hook()
    {
        if (isOnHook)
        {
            transform.position = hookAttached.position;
            transform.rotation = Quaternion.Euler(hookAttached.localRotation.eulerAngles.x, hookAttached.localRotation.eulerAngles.y, (int)((((transform.rotation.eulerAngles.z/90)%4)+4)%4)*90);
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    public void UnHook()
    {
        if (isOnHook)
        {
            rb.constraints = RigidbodyConstraints.None;
            hookAttached = null;
        }
    }

    IEnumerator CoroutineStopWata()
    {
        yield return stopTime;
        currentElement.StopParticles();
        currentElement.SetElementData(null);
        isFeed = false;
    }
}
