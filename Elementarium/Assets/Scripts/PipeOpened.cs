using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeOpened : MonoBehaviour
{
    [SerializeField] private List<PipeOpened> listPipeOpenedOrdered;
    [SerializeField] private Element element;
    [SerializeField] private bool isFilled = false;

    private void Start()
    {
        if (listPipeOpenedOrdered[0].Equals(this))
        {
            element.PlayParticles(transform, transform.rotation, transform);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("PipeOpenedShape"))
        {
           
            isFilled = true;
            element.StopParticles();
            
            for (int i = 0; i < listPipeOpenedOrdered.Count; i++)
            {
                if (!listPipeOpenedOrdered[i].isFilled)
                {
                    return;
                }
                
                if (listPipeOpenedOrdered[i].Equals(this))
                {
                    if (i<listPipeOpenedOrdered.Count-1)
                    {
                        listPipeOpenedOrdered[i+1].element.PlayParticles(listPipeOpenedOrdered[i+1].transform, listPipeOpenedOrdered[i+1].transform.rotation, listPipeOpenedOrdered[i+1].transform);
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("PipeOpenedShape"))
        {
            isFilled = false;
            for (int i = 0; i < listPipeOpenedOrdered.Count; i++)
            {
                if (listPipeOpenedOrdered[i].Equals(this))
                {
                    if (i.Equals(0) && i+1<listPipeOpenedOrdered.Count)
                    {
                        listPipeOpenedOrdered[i+1].element.StopParticles();
                        element.PlayParticles(transform, transform.rotation, transform);
                    }
                }
            }
        }
    }
}