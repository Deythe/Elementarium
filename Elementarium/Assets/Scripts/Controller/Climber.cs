using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Climber : MonoBehaviour
{
    [SerializeField] private CharacterController character;
    public static ActionBasedController climbingHand;
    [SerializeField] private ActionBasedContinuousMoveProvider continuousMovement;
    [SerializeField] private TeleportationProvider teleportMovement;

    void FixedUpdate()
    {
        if (climbingHand)
        {
            continuousMovement.enabled = false;
            Climb();
        }
        else if (!teleportMovement.enabled)
        {
            continuousMovement.enabled = true;
        }
    }

    private void Climb()
    {
        Vector3 velocity = climbingHand.hapticDeviceAction.action.ReadValue<Vector3>();
        Debug.Log("velocity");
        character.Move(-velocity * Time.deltaTime);
        Debug.Log("moving");
    }
}
