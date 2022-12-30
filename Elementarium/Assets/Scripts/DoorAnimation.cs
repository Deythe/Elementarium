using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimation : MonoBehaviour
{

    Animation doorState;
    void Start()
    {
        doorState = GetComponent<Animation>();
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
