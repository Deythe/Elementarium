using System;
using System.Collections;
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

    [Header("Finish")]
    [SerializeField] private float finishDelay;
    [SerializeField] private UnityEvent finishConditionEvent;

    [Header("Reset")]
    [SerializeField] private float resetDelay;
    [SerializeField] private UnityEvent resetCondition;

    bool completed;
    bool flag;
    int totalNbChange;
    int nbChange;

    void Start()
    {
        foreach (Transform activator in activators)
        {
            iCompleted = activator.GetComponent<ICompleted>();
            Debug.Log(iCompleted);
            if (iCompleted != null) activs.Add(iCompleted);
        }
    }

    public void CheckState()
    {
        completed = true;
        flag = true;
        nbChange = 0;
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

            if (activator.getResetCondition())
            {
                Debug.Log("reset");
                resetCondition.Invoke();
            }
        }

        if (nbChange > totalNbChange) totalNbChange = nbChange;

        StartCoroutine(CheckStateCoroutine());       
    }

    IEnumerator CheckStateCoroutine() 
    {
        if (hasOrder && totalNbChange > 1 && completed)
        {
            yield return new WaitForSeconds(resetDelay);
            resetCondition.Invoke();
            totalNbChange = 0;
        }
        else if (completed) 
        {
            yield return new WaitForSeconds(finishDelay);
            finishConditionEvent.Invoke();
        }
    }
}
