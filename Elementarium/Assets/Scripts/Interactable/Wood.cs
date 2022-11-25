using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood :Interactible
{
    private Element _collidedParticle;

    protected override void Collide(Element e)
    {
        Destroy(gameObject);
    }
}
