using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class HandController : MonoBehaviour
{
    [SerializeField] private Element _element;
    
    [SerializeField] private InputActionProperty _triggerAction;
    [SerializeField] private InputActionProperty _gripAction;
    [SerializeField] private AnimatedHandOnInput anim;
    [SerializeField] private Absorb _absorb;
    [SerializeField] private Expulse _expulse;
    [SerializeField] private XRRayInteractor _rayHand;
    [SerializeField] private bool debugMode;
    [SerializeField] private AudioSource source;
    [SerializeField] private Material empty;
    [SerializeField] private MeshRenderer gemMesh;
    
    private bool _haveAnElement, _haveObjectInHand, _haveGlove, _haveShot;

    private void Start()
    {
        haveGlove = debugMode;
    }

    public InputActionProperty triggerAction
    {
        get => _triggerAction;
        set
        {
            _triggerAction= value;
        }
    }

    public XRRayInteractor rayHand
    {
        get => _rayHand;
        set
        {
            _rayHand = value;
        }
    }
    
    public InputActionProperty gripAction
    {
        get => _gripAction;
        set
        {
            _gripAction= value;
        }
    }
    
    public bool haveObjectInHand
    {
        get => _haveObjectInHand;
        set
        {
            _haveObjectInHand = value;
            anim.handAnimator.SetBool("HaveObjectInHand", _haveObjectInHand);
        }
    }
    
    public bool haveGlove
    {
        get => _haveGlove;
        set
        {
            _haveGlove = value;
            _absorb.enabled = value;
            _expulse.enabled = value;
            anim.handAnimator.SetBool("HaveGlove", _haveGlove);
        }
    }
    
    public bool haveShot
    {
        get => _haveShot;
        set
        {
            _haveShot = value;
        }
    }

    public Element element
    {
        get => _element;
        set
        {
            value = _element;
        }
    }

    public void ResetElement()
    {
        gemMesh.material = empty;
        if (element != null)
        {
            _expulse.StopFire();
        }
        _element.SetElementData(null);
    }

    public void ChangeGemMesh()
    {
        gemMesh.material = element.GetElementData().GetMatBracelet();
    }

    public void PlaySound(AudioClip clip)
    {
        if (!source.isPlaying)
        {
            source.clip = clip;
            source.Play();
        }
    }

    public void StopSound()
    {
        source.Stop();
        source.clip = null;
        source.loop = false;
    }
}
