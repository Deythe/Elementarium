using UnityEngine;

public class Brasero : Interactible
{
    [SerializeField] Animation braseroDoor;
    [SerializeField] Animation emmisivePart;
    protected bool onFire;
    public float offset;
    protected GameObject fire;
    protected override void Collide(Transform collid)
    {
        SwitchOn();
    }

    public void SwitchOn()
    {
        if (onFire) return;

        onFire = true;
        fire = Pooler.instance.Pop("p_Fire", transform);
        fire.transform.position = transform.position + transform.up * offset;
        fire.transform.Rotate(transform.rotation.x - 90, 0, 0);
        fire.GetComponent<ParticleSystem>().Play();
        braseroDoor.Play("BraseroClose");
        emmisivePart.Play("BraseroEmissive");
        interactionEvent.Invoke();
    }
}
