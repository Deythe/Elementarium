using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    [SerializeField] private HandController leftHandController;
    [SerializeField] private HandController rightHandController;


    private void Awake()
    {
        instance = this;
    }

    public void ResetElements() 
    {
        Debug.Log("ResetElement in PlayerController called");
        leftHandController.ResetElement();
        rightHandController.ResetElement();
    }
}
