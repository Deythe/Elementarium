using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject player;
    [SerializeField] private InputActionProperty showButton,debugShowButton;
    [SerializeField] private Transform head;
    [SerializeField][Range(1,10)] private float spawnDistance = 2;
    [SerializeField] private XRRayInteractor leftRay, rightRay;
    
    [SerializeField] private ActionBasedContinuousMoveProvider continuousMove;
    [SerializeField] private TeleportationProvider teleportationMove;

    [SerializeField] private ActionBasedSnapTurnProvider snapTurn;
    [SerializeField] private ActionBasedContinuousTurnProvider continuousTurn;
    
    [SerializeField] private LayerMask UIlayer, TPLayer;
    
    [SerializeField] private List<Transform> TUTCP = new List<Transform>();
    [SerializeField] private List<Transform> WQCP = new List<Transform>();
    [SerializeField] private List<Transform> FQCP = new List<Transform>();
    
    private void Update()
    {
        if (showButton.action.WasPressedThisFrame() || debugShowButton.action.WasPressedThisFrame())
        {
            menu.SetActive(!menu.activeSelf);
            SwitchTypeRay();
            menu.transform.position = head.position + new Vector3(head.forward.x,0 , head.forward.z).normalized * spawnDistance;
        }
        
        menu.transform.LookAt(new Vector3(head.position.x, menu.transform.position.y, head.position.z));
        menu.transform.forward *= -1;
    }

    void SwitchTypeRay()
    {
        if (menu.activeSelf)
        {
            leftRay.lineType = XRRayInteractor.LineType.StraightLine;
            rightRay.lineType = XRRayInteractor.LineType.StraightLine;
        }
        else
        {
            leftRay.lineType = XRRayInteractor.LineType.ProjectileCurve;
            rightRay.lineType = XRRayInteractor.LineType.ProjectileCurve;
        }
    }
    
    public void SetTypeTurnFromIndex(int index)
    {
        if (index == 0)
        {
            leftRay.raycastMask = UIlayer;
            rightRay.raycastMask = UIlayer;
            teleportationMove.enabled = false;
            continuousMove.enabled = true;
        } else if (index == 1)
        {
            leftRay.raycastMask = TPLayer;
            rightRay.raycastMask = TPLayer;
            teleportationMove.enabled = true;
            continuousMove.enabled = false;
        }
    }
    
    public void SetTypeRotateFromIndex(int index)
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

    public void TeleportFromIndexTut(int index)
    {
        player.transform.position = TUTCP[index].position;
    }
    
    public void TeleportFromIndexWq(int index)
    {
        player.transform.position = WQCP[index].position;
    }
    
    public void TeleportFromIndexFq(int index)
    {
        player.transform.position = FQCP[index].position;
    }
}
