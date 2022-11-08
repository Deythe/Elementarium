using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : Element
{
    private void Start()
    {
        id = 1;
        priority = 3;
    }

    protected override void Merge(Element element)
    {
        switch (element.GetID()) 
        {
            case 2:
                Debug.Log("je fais un lance-flamme!");
                break;
            case 3:
                Debug.Log("je fais un bloc de terre cuite!");
                break;
            default:
                Debug.Log("Il y a un bug, c'est pas censé arriver !");
                break;
        }
    }
}
