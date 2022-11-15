using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandController : MonoBehaviour
{
    [SerializeField] private Element element;
    [SerializeField] private InputActionProperty _triggerAction;
    [SerializeField] private InputActionProperty _gripAction;
    
    private bool _haveAnElement, _haveInHand;
    
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
    
    public bool haveInHand
    {
        get => _haveInHand;
        set
        {
            _haveInHand = value;
        }
    }
    
    public bool haveAnElement
    {
        get => _haveAnElement;
        set
        {
            _haveAnElement = value;
        }
    }

    public Element GetElementData() 
    {
        return this.element;
    }

    public void ResetElement() 
    {
        element = null;
        Debug.Log("ResetElement in HandController called");
    }
}
