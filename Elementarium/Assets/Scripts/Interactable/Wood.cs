using System.Collections;
using UnityEngine;

public class Wood :Interactible
{

    [SerializeField] private MeshRenderer meshRenderer;
    private Material mat;
    [SerializeField] private Collider woodCollider;

    [Header("Burn Shader")]
    private Transform dissolveOrigin;
    [SerializeField] private AnimationCurve burnCurve;
    [SerializeField] private float burningTime;
    private WaitForEndOfFrame wait;

    private void Start()
    {
        wait = new WaitForEndOfFrame();
        mat = meshRenderer.material;
    }

    protected override void Collide(Transform collid)
    {
        SetDissolveOrigin(collid);
        interactionEvent.Invoke();
        Burn();
    }

    public void Burn()
    {
        woodCollider.enabled = false;
        mat.SetVector("_DissolveOrigin", dissolveOrigin.position);
        StartCoroutine(BurnCoroutine(burningTime));
    }

    private float i;
    IEnumerator BurnCoroutine(float t) 
    {
        i = 0;
        while (i < t) 
        {
            mat.SetFloat("_Dissolve", burnCurve.Evaluate(i / t));
            yield return wait;
            i += Time.deltaTime;
        }
        Destroy(gameObject);
    }

    public void SetDissolveOrigin(Transform dissolveOrigin) 
    {
        this.dissolveOrigin = dissolveOrigin;
    }
}
