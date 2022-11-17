using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Burn : MonoBehaviour
{

    private void OnParticleCollision(GameObject other)
    {
        Destroy(other);
    }
}
