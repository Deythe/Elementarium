using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimation : MonoBehaviour
{

    Animation doorState;
    [SerializeField] bool open;

    [SerializeField] private List<Transform> activators = new List<Transform>();
    private List<ICompleted> activs = new List<ICompleted>();
    private ICompleted iCompleted;

    void Start()
    {
        foreach (Transform activator in activators) 
        {
            iCompleted = activator.GetComponent<ICompleted>();
            if (iCompleted != null) activs.Add(iCompleted);
        }

        doorState = GetComponent<Animation>();

        if (open)
        {
            Open();
        }
    }

    public void CheckState() 
    {
        foreach (ICompleted activator in activs) 
        {
            if (!activator.getCompletedCondition()) 
            {
                if (open) Close();
                return;
            }
        }
        if (!open) Open();
        return;
    }

    public void Open()
    {
        doorState.Play("DoorOpen");
        open = true;
    }

    public void Close()
    {
        doorState.Play("DoorClose");
        open = false;
    }
}
