using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Water : Element
{
    protected override void Start()
    {
        base.Start();
        id = (int)ID.WATER;
        priority = Enum.GetValues(typeof(ID)).Cast<int>().Max() - id;
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

    protected void Steam() 
    {
        
    }

    protected void Ice() 
    {
        
    }

    protected void Mud() 
    {
        
    }

    protected override void Remove()
    {
        throw new NotImplementedException();
    }
}
