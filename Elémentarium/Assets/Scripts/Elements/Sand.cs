using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Sand", menuName = "Element/Sand")]
public class Sand : ElementData
{

    public override void Merge(ElementData elementData, Vector3 collisionPoint)
    {
        throw new NotImplementedException();
    }

    public override void Remove()
    {
        throw new NotImplementedException();
    }
}
