using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class Absorb : MonoBehaviour
{
    [SerializeField] private HandController hand;
    [SerializeField] private InputActionProperty gripAction;
    [SerializeField] private GameObject absorbObjects;
    public void Update()
    {
        if (gripAction.action.ReadValue<float>() > 0.5f)
        {
            absorbObjects.SetActive(true);
        }
        else
        {
            absorbObjects.SetActive(false);
        }
    }
}
