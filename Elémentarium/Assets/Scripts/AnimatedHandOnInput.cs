using UnityEngine;
using UnityEngine.InputSystem;

public class AnimatedHandOnInput : MonoBehaviour
{
    [SerializeField] private InputActionProperty triggerAnimationAction, gripAnimationAction;
    
    [SerializeField] private Animator _handAnimator;
    private float _triggerValue, _gripValue;
   
    // Update is called once per frame
    void Update()
    {
        _gripValue = gripAnimationAction.action.ReadValue<float>();
        _triggerValue = triggerAnimationAction.action.ReadValue<float>();
        _handAnimator.SetFloat("Trigger", _triggerValue);
        _handAnimator.SetFloat("Grip", _gripValue);
    }
}
