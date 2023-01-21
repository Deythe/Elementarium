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

    [Header("WaterShader")]
    private float firstTilingSpeed;
    private float secondTilingSpeed;

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
            firstTilingSpeed = mat.GetFloat("_FirstTilingSpeed");
            secondTilingSpeed = mat.GetFloat("_SecondTilingSpeed");
            Debug.Log("first" + firstTilingSpeed);
            Debug.Log("second" + secondTilingSpeed);
            StartCoroutine(FreezeCoroutine(solidificationTime));
        }
    }

    private float i;
    IEnumerator FreezeCoroutine(float t) 
    {
        i = 0;
        while (i < t)
        {
            mat.SetFloat("_IceDistance", solidificationCurve.Evaluate(i / t));
            mat.SetFloat("_FirstTilingSpeed", Mathf.Lerp(firstTilingSpeed, 0, i / t));
            mat.SetFloat("_SecondTilingSpeed", Mathf.Lerp(secondTilingSpeed, 0, i / t));
            yield return wait;
            i += Time.deltaTime;
        }
    }

}
