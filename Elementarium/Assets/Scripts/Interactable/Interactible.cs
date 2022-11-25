using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactible: MonoBehaviour
{
    private Element _collidedParticle;
    [SerializeField] private List<ElementData.ID> neededIDs;
    
    private void OnParticleCollision(GameObject other)
    {
        _collidedParticle = other.GetComponent<Element>();
        
        if (CheckCollidedElement())
        {
            Collide(_collidedParticle);
        }
        else
        {
            _collidedParticle = null;
        }
    }

    private bool CheckCollidedElement()
    {
        if (_collidedParticle != null)
        {
            return (neededIDs.Contains(_collidedParticle.GetID())) ;
        }

        return false;
    }

    protected abstract void Collide(Element e);
}
