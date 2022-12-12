using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brasero : Interactible
{
    private bool onFire;
    public Vector3 offset;
    private GameObject fire;
    protected override void Collide(Transform collid)
    {
        if (onFire) return;
        onFire = true;
        fire = Pooler.instance.Pop("p_Fire", transform);
        fire.transform.position = transform.position + offset;
        fire.transform.Rotate(-90,0,0);
        fire.GetComponent<ParticleSystem>().Play();
    }
}
