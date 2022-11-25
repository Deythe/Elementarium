using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class Expulse : MonoBehaviour
{
    [SerializeField] private HandController motherHand;
    [SerializeField] private Transform anchorTransform;
    
    private GameObject elementGO;
    private ParticleSystem elementPS;
    private RaycastHit hit;
    private bool hasShot;


    public void Update()
    {
        if (CheckIfInMenu())
        {
            motherHand.element.StopParticles();
            hasShot = false;
            return;
        }
        
        if(motherHand.haveObjectInHand) return;
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
        if (motherHand.triggerAction.action.ReadValue<float>() > 0.5f && motherHand.gripAction.action.ReadValue<float>()<0.05f)
        {
            if (!hasShot)
            {
                motherHand.element.PlayParticles(anchorTransform, anchorTransform);
                hasShot = true;
            }
        }
        else
        {
            motherHand.element.StopParticles();
            hasShot = false;
        }
    }
}
