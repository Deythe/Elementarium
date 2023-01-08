using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

public class FinishCondition : MonoBehaviour
{
    [SerializeField] private List<Transform> activators = new List<Transform>();
    private List<ICompleted> activs = new List<ICompleted>();
    private ICompleted iCompleted;

    [SerializeField] private UnityEvent finishConditionEvent;
    [SerializeField] private UnityEvent resetCondition;

    bool flag;

    void Start()
    {
        foreach (Transform activator in activators)
        {
            iCompleted = activator.GetComponent<ICompleted>();
            if (iCompleted != null) activs.Add(iCompleted);
        }
    }

    public void CheckState()
    {
        Debug.Log("CheckState");
        foreach (ICompleted activator in activs)
        {
            if (!activator.getCompletedCondition())
            {
                resetCondition.Invoke();
                return;
            }
        }
        finishConditionEvent.Invoke();
        return;
    }
}
