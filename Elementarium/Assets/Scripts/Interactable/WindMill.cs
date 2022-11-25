using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindMill: Interactible
{
    [SerializeField] private GameObject pales;
    
    protected override void Collide(Element e)
    {
        pales.transform.Rotate(Vector3.forward, 10);
    }
}
