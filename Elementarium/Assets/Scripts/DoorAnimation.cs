using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimation : MonoBehaviour
{

    Animation doorState;
    [SerializeField] bool open;
    void Start()
    {
        doorState = GetComponent<Animation>();

        if (open)
        {
            Open();
        }
    }

    public void Open()
    {
        doorState.Play("DoorOpen");
    }

    public void Close()
    {
        doorState.Play("DoorClose");
    }
}
