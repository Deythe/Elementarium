using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Source : MonoBehaviour
{

    [SerializeField] private ElementData element;

    
    
    public ElementData GetElement()
    {
        return element;
    }
}
