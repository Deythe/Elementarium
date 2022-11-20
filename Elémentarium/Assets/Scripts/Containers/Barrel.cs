using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour, IContainer
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float baseMass;
    private float currentMass;

    [SerializeField] private float maxCapacity;
    [SerializeField] private float currentCapacity;

    [SerializeField] private Element currentElement;


    private float rotation;
    [SerializeField] private float fillSpeed;
    [SerializeField] private float emptySpeed;
    private bool isEmptying = false;

    [SerializeField] private Transform particleStart;

    private Element collidedElement;

    private void Start()
    {
        Debug.Log(currentElement.GetMass());
        currentMass = baseMass + currentElement.GetMass() * currentCapacity;
        rb.mass = currentMass;
    }

    public float GetCurrentMass()
    {
        return this.currentMass;
    }

    public float GetCurrentCapacity() 
    {
        return this.currentCapacity;
    }

    public Element GetElementData() 
    {
        return this.currentElement;
    }

    public void ModifyCapacity(Element element, float quantity) 
    {
        if (currentElement.GetID() == element.GetID())
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
            rb.mass = currentMass;
        }
    }

    private void CheckRotation() 
    {
        rotation = Vector3.Angle(Vector3.up, transform.up) - 90;
        if (rotation > 0)
        {
            ModifyCapacity(currentElement, -(rotation / 90) * emptySpeed * Time.deltaTime);
            if (currentCapacity > 0 && !isEmptying)
            {
                currentElement.PlayParticles(particleStart, Quaternion.FromToRotation(Vector3.forward, particleStart.forward), transform);
                isEmptying = true;
            }
            else if(currentCapacity <= 0)
            {
                currentElement.StopParticles();
            }
        }
        else 
        {
            currentElement.StopParticles();
            isEmptying = false;
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("Collision");
        if ((collidedElement = other.GetComponentInParent<Element>()) != null) 
        {
            ModifyCapacity(collidedElement, fillSpeed);
        }
    }

    private void Update()
    {
        CheckRotation();
    }
}
