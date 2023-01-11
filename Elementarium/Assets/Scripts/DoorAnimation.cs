using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimation : MonoBehaviour
{

    [SerializeField] Animation doorState;
    [SerializeField] bool open;

    void Start()
    {

        if (open)
        {
            Open();
        }
    }

    public void Open()
    {
        
        if(!open) doorState.Play("DoorOpen");
        open = true;
    }

    public void Close()
    {
        if(open) doorState.Play("DoorClose");
        open = false;
    }
}
