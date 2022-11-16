using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Fire", menuName = "Element/Fire")]
public class Fire : ElementData
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
