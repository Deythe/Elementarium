using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepopOnCollision : MonoBehaviour
{
    [SerializeField] private Element element;

    private void OnCollisionEnter(Collision collision)
    {
        switch (element.GetID()) 
        {
            case ElementData.ID.LAVA:
                Destroy(transform.gameObject);
                break;
            //case ElementData.ID.MUD:

        }
    }
}
