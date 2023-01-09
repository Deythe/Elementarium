using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Lava", menuName = "Element/Lava")]
public class Lava : ElementData
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
