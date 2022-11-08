using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : Element
{
    private void Start()
    {
        id = 0;
        priority = 4;
    }

    protected override void Merge(Element element)
    {
        switch (element.GetID()) 
        {
            case 1:
                Debug.Log("Je fais de la vapeur!");
                break;
            case 2:
                Debug.Log("Je fais de la glace!");
                break;
            case 3:
                Debug.Log("Je fais de la boue!");
                break;
            default:
                Debug.Log("Il y a un bug, c'est pas censé arriver !");
                break;
        }
    }
}
