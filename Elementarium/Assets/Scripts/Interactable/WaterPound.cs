using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPound : Interactible
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Element element;
    [SerializeField] private Material waterMaterial;
    [SerializeField] private Material IceMaterial;
    [SerializeField] private ElementData iceData;

    protected override void Collide(Transform e)
    {
        Debug.Log("Pound collide");
        meshRenderer.material = IceMaterial;
        element.SetElementData(iceData);
    }
}
