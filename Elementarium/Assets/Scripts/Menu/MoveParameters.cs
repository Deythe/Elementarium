using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MoveParameters : MonoBehaviour
{
    [SerializeField]
    private ActionBasedContinuousMoveProvider continuousMove;
    
    [SerializeField]
    private TeleportationProvider teleportationMove;

    public void SetTypeFromIndex(int index)
    {
        if (index == 0) {
            teleportationMove.enabled = false;
            continuousMove.enabled = true;
        } else if (index == 1) {
            teleportationMove.enabled = true;
            continuousMove.enabled = false;
        }
    }
}
