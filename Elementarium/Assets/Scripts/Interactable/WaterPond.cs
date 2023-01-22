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
    private Vector2 firstTilingSpeed;
    private Vector2 secondTilingSpeed;

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
            firstTilingSpeed = mat.GetVector("_FirstTilingSpeed");
            secondTilingSpeed = mat.GetVector("_SecondTilingSpeed");
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
            mat.SetVector("_FirstTilingSpeed", new Vector2(Mathf.Lerp(firstTilingSpeed.x, 0, i / t), Mathf.Lerp(firstTilingSpeed.y, 0, i / t)));
            mat.SetVector("_SecondTilingSpeed", new Vector2(Mathf.Lerp(secondTilingSpeed.x, 0, i / t), Mathf.Lerp(secondTilingSpeed.y, 0, i / t)));
            yield return wait;
            i += Time.deltaTime;
        }
    }

}
