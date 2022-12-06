using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class TurnParameters : MonoBehaviour
{
    [SerializeField]
    private ActionBasedSnapTurnProvider snapTurn;
    
    [SerializeField]
    private ActionBasedContinuousTurnProvider continuousTurn;

    public void SetTypeFromIndex(int index)
    {
        if (index == 0) {
            snapTurn.enabled = false;
            continuousTurn.enabled = true;
        } else if (index == 1) {
            snapTurn.enabled = true;
            continuousTurn.enabled = false;
        } else if (index == 2){
            snapTurn.enabled = false;
            continuousTurn.enabled = false;
        }
    }
}