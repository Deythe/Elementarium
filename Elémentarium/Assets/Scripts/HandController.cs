using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    private bool _haveAnElement;
    [SerializeField] private Element element;

    public bool haveAnElement
    {
        get => _haveAnElement;
        set
        {
            _haveAnElement = value;
        }
    }

    public void ResetElement() 
    {
        element = null;
        Debug.Log("ResetElement in HandController called");
    }
}
