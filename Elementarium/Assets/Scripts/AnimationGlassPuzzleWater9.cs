using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationGlassPuzzleWater9 : MonoBehaviour
{

    [SerializeField] Animation glassAnim;
    [SerializeField] bool open;

    void Start()
    {
        if (open)
        {
            open = false;
            Open();
        }
    }

    public void Open()
    {
        if (!open) glassAnim.Play("GlassOpen");
        open = true;
    }

    public void Close()
    {
        if (open) glassAnim.Play("GlassClose");
        open = false;
    }
}
