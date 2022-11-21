using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Ice", menuName = "Element/Ice")]
public class Ice : ElementData
{

    public override void Merge(ElementData elementData, Vector3 collisionPoint, Quaternion collisionRotation)
    {
        throw new NotImplementedException();
    }

    public override void Remove()
    {
        throw new NotImplementedException();
    }
}
