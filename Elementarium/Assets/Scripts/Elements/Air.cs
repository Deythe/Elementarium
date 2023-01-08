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
        switch (elementData.GetID()) 
        {
            case ID.EARTH:
                Debug.Log("Merge Earth to Sand");
                MergeEarth(collisionPoint, collisionRotation);
                break;
        }
    }

    private void MergeEarth(Vector3 collisionPoint, Quaternion collisionRotation)
    {
        newElementGO = Pooler.instance.Pop("Sand", collisionPoint, collisionRotation);
        if ((element = newElementGO.GetComponent<Element>()) != null)
        {
            Debug.Log("Merge Earth");
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
