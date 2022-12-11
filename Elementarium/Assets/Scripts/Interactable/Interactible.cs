using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactible: MonoBehaviour
{
    private Element _collidedParticle;
    [SerializeField] private ElementData.ID neededID;
    
    private void OnParticleCollision(GameObject other)
    {
        _collidedParticle = other.GetComponentInParent<Element>();
        
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
            return (_collidedParticle.GetID() == neededID) ;
        }

        return false;
    }

    protected abstract void Collide(Element e);
}
