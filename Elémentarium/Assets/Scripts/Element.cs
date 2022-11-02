using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Elements")]
public class Element : ScriptableObject
{
    public string elementName;
    public int elementID;
    public float mass; //mass for 1m^3

    public float GetMass() 
    {
        return this.mass;
    }
}
