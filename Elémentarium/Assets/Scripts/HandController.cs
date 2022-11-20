using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandController : MonoBehaviour
{
    [SerializeField] private Element _element;
    [SerializeField] private InputActionProperty _triggerAction;
    [SerializeField] private InputActionProperty _gripAction;
    [SerializeField] private AnimatedHandOnInput anim;
    [SerializeField] private Absorb _absorb;
    [SerializeField] private Expulse _expulse;
    private bool _haveAnElement, _haveObjectInHand, _haveGlove;
    
    private void Start()
    {
        haveGlove = true;
    }

    public InputActionProperty triggerAction
    {
        get => _triggerAction;
        set
        {
            _triggerAction= value;
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
        _element = null;
        Debug.Log("ResetElement in HandController called");
    }
}
