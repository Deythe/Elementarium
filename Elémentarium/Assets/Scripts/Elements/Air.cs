using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Air : Element
{
    protected override void Start()
    {
        id = (int)ID.AIR;
        priority = Enum.GetValues(typeof(ID)).Cast<int>().Max() - id;
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

    protected void Sand() 
    {
        
    }

    protected override void Remove()
    {
        throw new NotImplementedException();
    }
}
