using System.Collections.Generic;
using UnityEngine;

public abstract class Interactible: MonoBehaviour
{
    private Transform _collidedParticle;
    [SerializeField] private List<ElementData.ID> neededID;
    
    private void OnParticleCollision(GameObject other)
    {
        _collidedParticle = other.transform;
        
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
        if (_collidedParticle.GetComponent<Element>() != null)
        {
            return neededID.Contains(_collidedParticle.GetComponent<Element>().GetID()) ;
        }

        return false;
    }

    protected abstract void Collide(Transform collid);

}
