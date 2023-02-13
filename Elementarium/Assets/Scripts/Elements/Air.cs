using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Air", menuName = "Element/Air")]
public class Air : ElementData
{
    private GameObject newElementGO;
    private Element element;

    public override void Merge(Transform elementCollided, ElementData elementData, Vector3 collisionPoint, Quaternion collisionRotation)
    {

    }

    public override void Remove()
    {
        throw new NotImplementedException();
    }
}
