using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour, IContainer
{
    [SerializeField] private float baseMass;
    private float currentMass;

    [SerializeField] private float maxCapacity;
    [SerializeField] private float currentCapacity;

    [SerializeField] private Element currentElement;

    private float rotation;
    [SerializeField] private float emptySpeed;

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

    public void ModifyCapacity(Element element, float quantity) 
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

            currentMass = baseMass + (currentCapacity * element.GetMass());
        }
    }

    private void CheckRotation() 
    {
        rotation = Vector3.Angle(Vector3.up, transform.up) - 90;
        if (rotation > 0) 
        {
            ModifyCapacity(currentElement, -(rotation / 90) * emptySpeed * Time.deltaTime);
        }
    }

    private void Update()
    {
        CheckRotation();
    }
}
