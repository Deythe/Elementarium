using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Earth : Element
{

    protected override void Start()
    {
        id = (int)ID.EARTH;
        priority = Enum.GetValues(typeof(ID)).Cast<int>().Max() - id;
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

    protected override void Remove()
    {
        throw new NotImplementedException();
    }
}
