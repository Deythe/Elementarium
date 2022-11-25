using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Water", menuName = "Element/Water")]
public class Water : ElementData
{

    private GameObject newElementGO;
    private Element newElement;

    public override void Merge(ElementData elementData, Vector3 collisionPoint, Quaternion collisionRotation)
    {
        switch (elementData.GetID()) 
        {
            case ID.FIRE:
                MergeFire(collisionPoint, collisionRotation);
                break;
            case ID.AIR:
                MergeAir(collisionPoint, collisionRotation);
                break;
        }
    }

    private void MergeFire(Vector3 collisionPoint, Quaternion collisionRotation)
    {
        newElementGO = Pooler.instance.Pop("Steam", collisionPoint, collisionRotation);
        if ((newElement = newElementGO.GetComponent<Element>()) != null)
        {
            newElement.PlayParticles();
            newElement.DelayedStopParticles(2);
        }
    }

    private void MergeAir(Vector3 collisionPoint, Quaternion collisionRotation) 
    {
        newElementGO = Pooler.instance.Pop("Ice", collisionPoint, collisionRotation);
        if ((newElement = newElementGO.GetComponent<Element>()) != null) 
        {
            newElement.PlayParticles();
            newElement.DelayedStopParticles(2);
        }
    }

    public override void Remove()
    {
        throw new NotImplementedException();
    }

    
}
