using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    [SerializeField] private HandController leftHandController;
    [SerializeField] private HandController rightHandController;
    [SerializeField] private DataPlayerScriptable data;
    [SerializeField] private GameObject rightBracelet, leftBracelet;
    [SerializeField] private Image blackScreen;
    
    
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

    private void Start()
    {
        StartCoroutine(CoroutineBlackScreen());
    }

    public void ResetElements() 
    {
        Debug.Log("ResetElement in PlayerController called");
        leftHandController.ResetElement();
        rightHandController.ResetElement();
    }

    public void GiveGlove()
    {
        rightBracelet.SetActive(true);
        leftBracelet.SetActive(true);
        leftHandController.haveGlove = true;
        rightHandController.haveGlove = true;
    }
    
    IEnumerator CoroutineBlackScreen()
    {
        while (blackScreen.color.a<=1)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b,
                blackScreen.color.a - 0.025f);
            yield return new WaitForSeconds(0.05f);
        }
    }
}
