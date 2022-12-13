using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

public class FinishCondition : MonoBehaviour
{
    [SerializeField] private Object condition;
    [SerializeField] private UnityEvent finishConditionEvent;
    [SerializeField] private UnityEvent resetCondition;

    bool flag;

    private void Update()
    {
        if (condition.GetComponent<ICompleted>().getCompletedCondition() && !flag)
        {
            flag = true;
            finishConditionEvent.Invoke();
            //Destroy(this);
        }
        else if(!condition.GetComponent<ICompleted>().getCompletedCondition() && flag)
        {
            flag = false;
            resetCondition.Invoke();
        }
    }
}
