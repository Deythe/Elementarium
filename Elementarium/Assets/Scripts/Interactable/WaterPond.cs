using System.Collections;
using UnityEngine;

public class WaterPond : Interactible
{

    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Element element;
    [SerializeField] private bool isSolid = false;

    [Header("IceShader")]
    private Material mat;
    private Transform solidificationOrigin;
    [SerializeField] private AnimationCurve solidificationCurve;
    [SerializeField] private float solidificationTime;
    private WaitForEndOfFrame wait;

    [SerializeField] private ElementData iceData;

    private void Start()
    {
        wait = new WaitForEndOfFrame();
        mat = meshRenderer.material;
    }

    protected override void Collide(Transform e)
    {
        solidificationOrigin = e;
        Freeze();
    }

    public void Freeze() 
    {
        if (!isSolid)
        {
            element.SetElementData(iceData);
            mat.SetVector("_IceOrigin", solidificationOrigin.position);
            StartCoroutine(FreezeCoroutine(solidificationTime));
        }
    }

    private float i;
    IEnumerator FreezeCoroutine(float t) 
    {
        i = 0;
        Debug.Log("in coroutine, outside while");
        while (i < t)
        {
            Debug.Log("in coroutine, inside while");
            mat.SetFloat("_IceDistance", solidificationCurve.Evaluate(i / t));
            yield return wait;
            i += Time.deltaTime;
        }
    }

}
