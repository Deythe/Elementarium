using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Water", menuName = "Element/Water")]
public class Water : ElementData
{
    public override void Merge(ElementData elementData)
    {
        switch (elementData.GetID()) 
        {
            case ID.FIRE:
                break;
        }
    }

    public override void Remove()
    {
        throw new NotImplementedException();
    }

    
}
