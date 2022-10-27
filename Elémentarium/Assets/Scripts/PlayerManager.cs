using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    [SerializeField] private HandController leftHandController;
    [SerializeField] private HandController rightHandController;
    [SerializeField] private DataPlayerScriptable data;

    public DataPlayerScriptable p_data
    {
        get => data;
        set
        {
            data = value;
        }
    }

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
