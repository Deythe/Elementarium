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
    [SerializeField] private bool hasOrder;

    [SerializeField] private UnityEvent finishConditionEvent;
    [SerializeField] private UnityEvent resetCondition;

    bool completed;
    bool flag;
    int nbChange;

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
        completed = true;
        flag = true;
        nbChange = 0;
        Debug.Log("CheckState");
        foreach (ICompleted activator in activs)
        {
            if (flag != activator.getCompletedCondition()) 
            {
                nbChange++;
                flag = !flag;
            }
            if (!activator.getCompletedCondition())
            {
                completed = false;
            }
        }

        if (completed) finishConditionEvent.Invoke();
        else if(hasOrder && nbChange > 1) resetCondition.Invoke();
    }
}
