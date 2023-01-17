using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class Expulse : MonoBehaviour
{
    [SerializeField] private HandController motherHand;
    [SerializeField] private Transform anchorTransform;
    
    private GameObject elementGO;
    private ParticleSystem elementPS;
    private RaycastHit hit;


    public void Update()
    {
        if (CheckIfInMenu())
        {
            motherHand.element.StopParticles();
            motherHand.haveShot = false;
            return;
        }
        
        FireElement();

    }

    private bool CheckIfInMenu()
    {
        return motherHand.rayHand.TryGetHitInfo(out Vector3 pos, out Vector3 normal, out int number, out bool valid);
    }

    private void FireElement()
    {
        if (motherHand.element == null) return;
        if(motherHand.element.GetElementData() == null) return;
        
        if (motherHand.triggerAction.action.ReadValue<float>() > 0.5f && !motherHand.haveObjectInHand)
        {
            if (motherHand.gripAction.action.ReadValue<float>() < 0.05f)
            {
                if (!motherHand.haveShot)
                { 
                    motherHand.element.PlayParticles(anchorTransform, anchorTransform);
                    motherHand.haveShot = true;
                }
            }
            else if(motherHand.gripAction.action.ReadValue<float>()>0.50)
            {
                StopFire();
            }
        }
        else
        {
            StopFire();
        }
    }

    private void StopFire()
    {
        if (motherHand.haveShot)
        {
            if (!motherHand.haveObjectInHand)
            {
                motherHand.element.StopParticles();
            }
            else
            {
                motherHand.element.DetacheFromHand();
            }

            motherHand.haveShot = false;
        }
    }
}
