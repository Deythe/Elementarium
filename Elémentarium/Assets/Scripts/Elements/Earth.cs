using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth : Element
{
    private void Start()
    {
        id = 3;
        priority = 1;
    }

    protected override void Merge(Element element)
    {
        switch (element.GetID()) 
        {
            default:
                Debug.Log("Il y a un bug, c'est pas censé arriver !");
                break;
        }
    }
}
