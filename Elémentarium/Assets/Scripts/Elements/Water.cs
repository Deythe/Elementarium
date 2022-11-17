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

    public override void Merge(ElementData elementData, Vector3 collisionPoint)
    {
        Debug.Log("JE MERGE");
        switch (elementData.GetID()) 
        {
            case ID.FIRE:
                MergeFire(collisionPoint);
                break;
        }
    }

    private void MergeFire(Vector3 collisionPoint)
    {
        Debug.Log("JE FAIS DE LA VAPEUR");
        newElementGO = Pooler.instance.Pop("Steam", collisionPoint);
        if ((newElement = newElementGO.GetComponent<Element>()) != null)
        {
            newElement.PlayParticles();
        }
    }

    public override void Remove()
    {
        throw new NotImplementedException();
    }

    
}
