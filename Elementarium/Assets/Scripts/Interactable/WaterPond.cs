using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class WaterPond : Interactible
{

    [SerializeField] private WaterPondMaster waterPondMaster;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Element element;
    [SerializeField] private Material IceMaterial;
    [SerializeField] private ElementData iceData;

    protected override void Collide(Transform e)
    {
        Debug.Log("Pound collide");
        waterPondMaster.FreezeAllPonds();
    }

    public void Freeze() 
    {
        meshRenderer.material = IceMaterial;
        element.SetElementData(iceData);
    }

}
