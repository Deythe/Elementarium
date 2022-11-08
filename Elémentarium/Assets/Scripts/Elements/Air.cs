using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Air : Element
{
    private void Start()
    {
        id = 2;
        priority = 2;
    }

    protected override void Merge(Element element)
    {
        switch (element.GetID()) 
        {
            case 3:
                Debug.Log("je fais du sable!");
                break;
            default:
                Debug.Log("Il y a un bug, c'est pas censé arriver !");
                break;
        }
    }
}
