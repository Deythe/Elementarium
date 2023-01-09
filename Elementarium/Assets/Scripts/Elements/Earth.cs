using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Earth", menuName = "Element/Earth")]
public class Earth : ElementData
{
    public override void Merge(Transform elementCollided, ElementData elementData, Vector3 collisionPoint, Quaternion collisionRotation)
    {

    }

    public override void Remove()
    {
        throw new NotImplementedException();
    }
}
