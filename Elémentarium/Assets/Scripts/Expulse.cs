using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class Expulse : MonoBehaviour
{
    [SerializeField] private HandController motherHand;
    [SerializeField] private XRRayInteractor rayHand;

    private GameObject elementGO;
    private ParticleSystem elementPS;
    private Transform parentController;
    private RaycastHit hit;
    private bool hasShot;
    
    
    private void Start()
    {
        parentController = motherHand.transform;
        //motherHand = GetComponent<Element>();
    }
    
    public void Update()
    {
        if (CheckIfInMenu()) return;
        FireElement();
    }

    private bool CheckIfInMenu()
    {
        return rayHand.TryGetHitInfo(out Vector3 pos, out Vector3 normal, out int number, out bool valid);
    }

    private void FireElement()
    {
        if (motherHand.element == null) return;
        
        if(motherHand.element.GetElementData() == null) return;
        if (motherHand.triggerAction.action.ReadValue<float>() > 0.5f && motherHand.gripAction.action.ReadValue<float>()<0.1f)
        {
            if (!hasShot)
            {
                motherHand.element.PlayParticles(transform, transform);
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
