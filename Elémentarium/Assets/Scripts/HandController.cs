using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    private bool _haveAnElement;

    public bool haveAnElement
    {
        get => _haveAnElement;
        set
        {
            _haveAnElement = value;
        }
    }
}
