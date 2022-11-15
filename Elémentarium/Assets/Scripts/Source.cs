using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Source : MonoBehaviour
{

    [SerializeField] private ParticleSystem element;

    // Update is called once per frame
    public ParticleSystem GetElement()
    {
        return element;
    }
}
