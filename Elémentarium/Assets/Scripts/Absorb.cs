using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Absorb : MonoBehaviour
{
    [SerializeField] private HandController hand;
    [SerializeField] private InputActionProperty gripAction;
    [SerializeField] private GameObject AbsorbObjects;

    public void Update()
    {
        if (gripAction.action.ReadValue<float>() > 0.5f)
        {
            if (!hand.haveAnElement)
            {
                AbsorbObjects.SetActive(true);       
            }
        }
        else
        {
            AbsorbObjects.SetActive(false);
        }
    }
}
