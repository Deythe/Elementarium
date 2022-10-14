using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimatedHandOnInput : MonoBehaviour
{
    [SerializeField] private InputActionProperty pinchAnimationAction, gripAnimationAction;
    
    [SerializeField] private Animator _handAnimator;
    private float _triggerValue, _gripValue;
   
    // Update is called once per frame
    void Update()
    {
        _gripValue = gripAnimationAction.action.ReadValue<float>();
        _triggerValue = pinchAnimationAction.action.ReadValue<float>();
        _handAnimator.SetFloat("Trigger", _triggerValue);
        _handAnimator.SetFloat("Grip", _gripValue);
    }
}
