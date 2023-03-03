using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamesaberPillar : MonoBehaviour
{
    public float speed;
    public AnimationCurve speedOverTime;
    public float lifetime;
    public float distance;
    public float maxDistance;

    public FlamesaberManager FS_Manager;
    public string prefabName;

    private bool activated;


    void Start()
    {
        lifetime = 0;
        distance = 0;

        activated = false;
    }


    void Update()
    {
        lifetime += Time.deltaTime;
        transform.Translate(Vector3.forward * speed * speedOverTime.Evaluate(lifetime) * Time.deltaTime);
        //distance = lifetime * speed; //DIDN'T WORK WITH CURVE
        distance += Time.deltaTime * speed * speedOverTime.Evaluate(lifetime);

        if (distance > maxDistance) Depop();
    }

    public void Activated()
    {
        activated = true;
    }

    void Depop()
    {
        lifetime = 0;
        distance = 0;

        if (activated) FS_Manager.AddScore();
        else FS_Manager.StopPuzzle();

        activated = false;

        Pooler.instance.DePop(prefabName, gameObject);
    }


    [ContextMenu("Debug Puzzle")]
    public void DebugActivate()
    {
        Activated();
    }
}
