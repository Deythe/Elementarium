using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BraseroAnimation : MonoBehaviour
{

    [SerializeField] Animation braseroDoor;
    [SerializeField] Animation emmisivePart;
    public void CloseBrasero() 
    {
        braseroDoor.Play("BraseroClose");
        emmisivePart.Play("BraseroEmissive");
    }

}
