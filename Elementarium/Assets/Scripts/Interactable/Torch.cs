using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : Brasero, ICompleted
{
    [SerializeField] private bool finalState;

    [ContextMenu("Toggle Torch")]
    public void ToggleFire(bool invokeEvt) 
    {
        if (!onFire) SwitchOn(invokeEvt);
        else SwitchOff(invokeEvt);
    }

    public bool getCompletedCondition()
    {
        return onFire == finalState;
    }

    public bool getResetCondition()
    {
        return false;
    }
}
