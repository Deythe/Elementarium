using UnityEngine;

public class Wood :Interactible
{

    protected override void Collide(Transform collid)
    {
        interactionEvent.Invoke();
        Burn();
    }

    public void Burn()
    {
        Destroy(gameObject);
    }
}
