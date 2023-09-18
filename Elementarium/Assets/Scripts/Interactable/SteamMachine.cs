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
    [SerializeField] private AudioSource _source;
    private bool cooling;
    private bool frozen;
    private float angle;

    private bool isOn;
    

    private void Update()
    {
        angle = arrow.localEulerAngles.x;
        angle = (angle > 180) ? angle - 360 : angle;
        if (!cooling) return;
        Cool();
    }

    private void Cool()
    {
        if (angle >= -80)
        {
            arrow.Rotate(-coolingSpeed * Time.deltaTime, 0, 0);

            if (angle <= -80)
            {
                arrow.Rotate(coolingSpeed * Time.deltaTime, 0, 0);
                cooling = false;
            }
            else if (isOn && angle < -40)
            {
                interactionEvent.Invoke();
                isOn = false;
            }
        }
    }

    private void Heat()
    {
        if (angle <= 40)
        {
            arrow.Rotate(heatingSpeed * Time.deltaTime, 0, 0);
            if (angle >= 40)
            {
                arrow.Rotate(-heatingSpeed * Time.deltaTime, 0, 0);
            }
            else if (!isOn && angle > -40)
            {
                interactionEvent.Invoke();
                isOn = true;
            }
            cooling = true;
        }
    }

    protected override void Collide(Transform e)
    {
        if (e.GetComponentInParent<Element>().GetID() == ElementData.ID.STEAM)
        {
            Heat();
        } 
        else if (e.GetComponentInParent<Element>().GetID() == ElementData.ID.ICE)
        {
            if (frozen) return;
            StartCoroutine(Freeze());
        }
        else
        {
            arrow.Rotate(-heatingSpeed, 0, 0);
        }
        
    }

    private IEnumerator Freeze()
    {
        frozen = true;
        yield return new WaitForSeconds(2);
        frozen = false;
    }

    public bool getCompletedCondition()
    {
        return angle is >= -40 and <= 40;
    }

    public bool getResetCondition()
    {
        return angle is <= -40 ;
    }
}
