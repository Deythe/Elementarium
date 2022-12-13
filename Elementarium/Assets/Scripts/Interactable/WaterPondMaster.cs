using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPondMaster : MonoBehaviour
{
    [SerializeField] private List<WaterPond> ponds;

    public void FreezeAllPonds() 
    {
        foreach (WaterPond wp in ponds) 
        {
            wp.Freeze();
        }
    }
}
