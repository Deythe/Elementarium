using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour, IContainer, ISource
{
    [SerializeField] private float baseMass;
    private float currentMass;

    [SerializeField] private float maxCapacity;
    [SerializeField] private float currentCapacity;

    [SerializeField] private ElementData currentElement;

    [SerializeField] private float fillSpeed;

    private float rotation;
    [SerializeField] private float emptySpeed;

    [SerializeField] private LayerMask fillLayerMask;

    [SerializeField] private ParticleSystem particles;

    private void Start()
    {
        currentMass = baseMass + currentElement.mass * currentCapacity;
    }

    public float GetCurrentMass()
    {
        return this.currentMass;
    }

    public float GetCurrentCapacity() 
    {
        return this.currentCapacity;
    }

    public ElementData GetElementData() 
    {
        return this.currentElement;
    }

    public void ModifyCapacity(ElementData element, float quantity) 
    {
        if (currentElement.elementID == element.elementID)
        {
            if (quantity + currentCapacity > maxCapacity)
            {
                currentCapacity = maxCapacity;
            }
            else if (quantity + currentCapacity < 0)
            {
                currentCapacity = 0;
            }
            else
            {
                currentCapacity += quantity;
            }

            currentMass = baseMass + (currentCapacity * element.mass);
        } 
    }

    private void CheckRotation() 
    {
        rotation = Vector3.Angle(Vector3.up, transform.up) - 90;
        if (rotation > 0)
        {
            ModifyCapacity(currentElement, -(rotation / 90) * emptySpeed * Time.deltaTime);
            if (currentCapacity > 0)
            {
                particles.Play();
            }
            else 
            {
                particles.Stop();
            }
        }
        else 
        {
            particles.Stop();
        }
    }

    private void Update()
    {
        CheckRotation();
    }

    private ISource source;
    private void OnParticleCollision(GameObject other)
    {
        if ((fillLayerMask & (1<< other.gameObject.layer)) != 0)
        {
            source = GetComponentInParent<ISource>();
            if (source != null)
            {
                ModifyCapacity(source.GetElementData(), fillSpeed);
            }
        }
    }
}
