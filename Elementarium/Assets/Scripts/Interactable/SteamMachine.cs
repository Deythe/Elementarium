using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SteamMachine : Interactible, ICompleted
{
    [SerializeField] private Transform arrow;
    [SerializeField] private float coolingSpeed;
    [SerializeField] private float heatingSpeed;
    private bool cooling;
    private bool frozen;
    private float angle;
    

    private void Update()
    {
        angle = arrow.localEulerAngles.x;
        angle = (angle > 180) ? angle - 360 : angle;
        if (!cooling) return;
        if (angle >= -80)
        {
            arrow.Rotate(-coolingSpeed, 0, 0);
            interactionEvent.Invoke();
            if (angle <= -80)
            {
                arrow.Rotate(coolingSpeed, 0, 0);
                cooling = false;
            }
        }
        
    }

    protected override void Collide(Transform e)
    {
        if (e.GetComponentInParent<Element>().GetID() == ElementData.ID.STEAM)
        {
            if (angle <= 80)
            {
                arrow.Rotate(heatingSpeed, 0, 0);
                interactionEvent.Invoke();
                if (angle >= 80)
                {
                    arrow.eulerAngles = new Vector3(350, 90, 0);
                }
                cooling = true;
            }
        } else
        {
            arrow.Rotate(-heatingSpeed, 0, 0);
            if (frozen) return;
            StartCoroutine(Freeze());
        }
    }

    private IEnumerator Freeze()
    {
        Debug.Log("I'm frozen");
        frozen = true;
        yield return new WaitForSeconds(2);
        frozen = false;
        Debug.Log("I'm not frozen anymore");
    }

    public bool getCompletedCondition()
    {
        return angle is >= -40 and <= 40;
    }

    public bool getResetCondition()
    {
        return !(angle is >= -40 and <= 40);
    }
}
