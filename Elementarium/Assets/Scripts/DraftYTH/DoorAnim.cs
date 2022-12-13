using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnim : MonoBehaviour
{
    private Animation doorAnim;
    // Start is called before the first frame update
    void Start()
    {
        doorAnim = GetComponent<Animation>();
    }

    public void Open()
    {
        doorAnim.Play();
    }
}
