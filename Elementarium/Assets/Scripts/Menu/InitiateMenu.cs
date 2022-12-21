using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class InitiateMenu : MonoBehaviour
{
    [SerializeField] private ActionBasedSnapTurnProvider snapTurn;
    [SerializeField] private ActionBasedContinuousTurnProvider continuousTurn;
    [SerializeField] private ActionBasedContinuousMoveProvider continuousMove;
    [SerializeField] private TeleportationProvider teleportationMove;
    [SerializeField] private TMP_Dropdown turnDD;
    [SerializeField] private TMP_Dropdown moveDD;

    private void Start()
    {
        if (snapTurn)
        {
            turnDD.value = 1;
        } else if (continuousTurn)
        {
            turnDD.value = 0;
        }
        else
        {
            turnDD.value = 2;
        }

        if (continuousMove)
        {
            moveDD.value = 0;
        }
        else
        {
            moveDD.value = 1;
        }
    }
}
