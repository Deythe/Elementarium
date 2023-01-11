using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : Brasero, ICompleted
{
    [SerializeField] private bool finalState;

    [ContextMenu("Toggle Torch")]
    public void ToggleFire() 
    {
        if (!onFire) SwitchOn();
        else SwitchOff();
    }

    public void SwitchOff() 
    {
        if (!onFire) return;


        onFire = false;

        Pooler.instance.DePop("p_Fire", fire);
        interactionEvent.Invoke();
    }

    public bool getCompletedCondition()
    {
        return onFire == finalState;
    }
}
