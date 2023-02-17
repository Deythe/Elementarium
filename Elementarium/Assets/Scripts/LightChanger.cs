using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightChanger : MonoBehaviour
{

    [SerializeField] private Light objLight;
    [SerializeField] private MeshRenderer objRenderer;
    [SerializeField] private Gradient gradient;
    [SerializeField] private AnimationCurve intensityCurve;
    [SerializeField] private AnimationCurve mEmissionIntensityCurve;

    private float time;
    private WaitForEndOfFrame wait = new WaitForEndOfFrame();


    public void ChangeLight(float duration) 
    {
        StartCoroutine(ChangeLightCoroutine(duration));
    }

    IEnumerator ChangeLightCoroutine(float t) 
    {
        time = 0;

        while (time < 1) 
        {
            objLight.color = gradient.Evaluate(time);
            objRenderer.material.SetColor("_EmissionColor", mEmissionIntensityCurve.Evaluate(time) * new Vector4(gradient.Evaluate(time).r, gradient.Evaluate(time).g, gradient.Evaluate(time).b));
            objLight.intensity = intensityCurve.Evaluate(time);

            yield return wait;

            time += Time.deltaTime / t;
        }
    }
}
