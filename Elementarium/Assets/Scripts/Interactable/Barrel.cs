using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Barrel :Interactible, IContainer, ICompleted
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
    private Transform camera;

    [SerializeField] private RectTransform canvas;
    [SerializeField] private TMP_Text text;
    
    private void Start()
    {
        currentMass = baseMass + currentElement.GetMass() * currentCapacity;
        if (Camera.main != null) camera = Camera.main.transform;
        rb.mass = currentMass;
    }

    public float GetCurrentMass()
    {
        return currentMass;
    }

    public float GetCurrentCapacity() 
    {
        return currentCapacity;
    }

    public Element GetElementData() 
    {
        return currentElement;
    }

    public void ModifyCapacity(Element element, float quantity) 
    {
        if (currentElement.GetID() == element.GetID())
        {
            if (quantity + currentCapacity >= maxCapacity)
            {
                currentCapacity = maxCapacity;
            }
            else if (quantity + currentCapacity <= 0)
            {
                currentCapacity = 0;
            }
            else
            {
                if ((quantity > 0 && !isEmptying) || quantity < 0)
                {
                    currentCapacity += quantity;
                }
            }

            currentMass = baseMass + (currentCapacity * element.GetMass());
            rb.mass = currentMass;
            rb.WakeUp();
            interactionEvent.Invoke();
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

    protected override void Collide(Transform e)
    {
        collidedElement = e.GetComponent<Element>();
        ModifyCapacity(collidedElement, fillSpeed);
    }

    private void Update()
    {
        CheckRotation();

        canvas.LookAt(new Vector3(camera.position.x, canvas.transform.position.y, camera.position.z));
        canvas.transform.forward *= -1;
        text.text = $"{currentCapacity}/{maxCapacity}";
    }

    public bool getCompletedCondition()
    {
        return (currentCapacity - maxCapacity).Equals(0);
    }
}
