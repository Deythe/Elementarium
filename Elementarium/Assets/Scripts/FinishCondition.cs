using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

public class FinishCondition : MonoBehaviour
{
    [SerializeField] private Object condition;
    [SerializeField] private UnityEvent finishConditionEvent;
    private void Update()
    {
        if (condition.GetComponent<ICompleted>().getCompletedCondition())
        {
            finishConditionEvent.Invoke();
            Destroy(this);
        }
    }
}
