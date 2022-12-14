using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnim : MonoBehaviour
{
    private Animation doorAnim;
    [SerializeField] bool open;
    // Start is called before the first frame update
    void Start()
    {
        doorAnim = GetComponent<Animation>();
        if (open)
        {
            doorAnim.Play("DoorTest");
        }
        else
        {
            doorAnim.Play("DoorTestClose");
        }
    }

    public void Open()
    {
        doorAnim.Play("DoorTest");
    }
    public void Close()
    {
        doorAnim.Play("DoorTestClose");
    }
}
