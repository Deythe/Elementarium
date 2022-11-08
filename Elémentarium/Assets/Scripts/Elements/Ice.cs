using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Ice : Element
{

    protected override void Start()
    {
        id = (int)ID.ICE;
        priority = Enum.GetValues(typeof(ID)).Cast<int>().Max() - id;
    }

    protected override void Merge(Element element)
    {
        throw new System.NotImplementedException();
    }

    protected override void Remove()
    {
        throw new System.NotImplementedException();
    }
}
