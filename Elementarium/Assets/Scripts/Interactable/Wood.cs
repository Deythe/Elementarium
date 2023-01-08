using UnityEngine;

public class Wood :Interactible
{
    private Element _collidedParticle;

    protected override void Collide(Transform collid)
    {
        interactionEvent.Invoke();
        Destroy(gameObject);
    }
    
}
