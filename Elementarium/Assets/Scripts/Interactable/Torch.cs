using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : Brasero, ICompleted
{
    [ContextMenu("Toggle Torch")]
    public void ToggleFire() 
    {
        if (!onFire) SwitchOn();
        else SwitchOff();
    }

    protected void SwitchOff() 
    {
        if (!onFire) return;


        onFire = false;

        Pooler.instance.DePop("p_Fire", fire);
    }

    public bool getCompletedCondition()
    {
        return onFire;
    }
}
