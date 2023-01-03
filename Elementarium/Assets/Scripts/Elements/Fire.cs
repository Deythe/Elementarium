using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Fire", menuName = "Element/Fire")]
public class Fire : ElementData
{

    private GameObject newElementGO;
    private Element element;

    public override void Merge(ElementData elementData, Vector3 collisionPoint, Quaternion collisionRotation)
    {
        switch (elementData.GetID())
        {
            case ID.AIR:
                MergeAir(collisionPoint, collisionRotation);
                Debug.Log("Merge Air");
                break;
            case ID.EARTH:
                MergeEarth(collisionPoint, collisionRotation);
                Debug.Log("Merge Earth to Lava");
                break;
        }
    }

    private void MergeAir(Vector3 collisionPoint, Quaternion collisionRotation)
    {

        newElementGO = Pooler.instance.Pop("Flamethrower", collisionPoint, collisionRotation);
        if ((element = newElementGO.GetComponent<Element>()) != null)
        {
            element.PlayParticles();
            element.DelayedStopParticles(2);
            element.DelayedDepopThis(2);
        }
    }

    private void MergeEarth(Vector3 collisionPoint, Quaternion collisionRotation)
    {
        newElementGO = Pooler.instance.Pop("Lava", collisionPoint, collisionRotation);
        if ((element = newElementGO.GetComponent<Element>()) != null) 
        {
            element.PlayParticles();
            element.DelayedStopParticles(2);
            element.DelayedDepopThis(2);
        }
    }

    public override void Remove()
    {
        throw new NotImplementedException();
    }
}
