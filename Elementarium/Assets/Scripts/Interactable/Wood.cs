using UnityEngine;

public class Wood :Interactible
{
    private Element _collidedParticle;

    protected override void Collide(Transform collid)
    {
        Destroy(gameObject);
    }
    
}
