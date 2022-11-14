using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Fire : Element
{
    protected override void Start()
    {
        id = (int)ID.FIRE;
        priority = Enum.GetValues(typeof(ID)).Cast<int>().Max() - id;
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

    protected void Flamethrower() 
    {
        
    }

    protected void Clay() 
    {
        
    }

    protected override void Remove()
    {
        throw new NotImplementedException();
    }
}
