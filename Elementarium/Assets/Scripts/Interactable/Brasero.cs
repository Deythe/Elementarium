using UnityEngine;

public class Brasero : Interactible
{
    [SerializeField] Animation braseroDoor;
    [SerializeField] Animation emmisivePart;
    [SerializeField] private AudioSource source;
    protected bool onFire;
    public float offset;
    protected GameObject fire;
    protected override void Collide(Transform collid)
    {
        SwitchOn(true);
    }

    public void SwitchOn(bool invokeEvt = true)
    {
        if (onFire) return;

        onFire = true;
        fire = Pooler.instance.Pop("p_Fire", transform);
        fire.transform.position = transform.position + transform.up * offset;
        fire.transform.Rotate(transform.rotation.x - 90, 0, 0);
        fire.GetComponent<ParticleSystem>().Play();
        if (braseroDoor!=null && emmisivePart!=null)
        {
            braseroDoor.Play("BraseroClose");
            emmisivePart.Play("BraseroEmissive");
            source.Play();
        }
        if (invokeEvt) 
        {
            Debug.Log("Event Invoked");
            interactionEvent.Invoke();
        }
        
    }

    public void SwitchOff(bool invokeEvt = true)
    {
        if (!onFire) return;

        onFire = false;
        Pooler.instance.DePop("p_Fire", fire);

        if (invokeEvt) 
        {
            Debug.Log("Event Invoked");
            interactionEvent.Invoke();
        } 
    }

}
