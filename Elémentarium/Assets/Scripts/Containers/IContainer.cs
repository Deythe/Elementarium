using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IContainer
{
    public float GetCurrentMass();
    public float GetCurrentCapacity();
    public void ModifyCapacity(ElementData element, float quantity);
}
