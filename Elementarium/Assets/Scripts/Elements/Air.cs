using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Air", menuName = "Element/Air")]
public class Air : ElementData
{
    public override void Merge(ElementData elementData, Vector3 collisionPoint, Quaternion collisionRotation)
    {
        switch (elementData.GetID()) 
        {
            case ID.EARTH:
                break;
        }
        Debug.Log("why the fuck is this merging with the void");
    }

    public override void Remove()
    {
        throw new NotImplementedException();
    }
}
