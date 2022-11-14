using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Sand : Element
{

    protected override void Start()
    {
        id = (int)ID.SAND;
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
