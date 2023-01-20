using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] private Transform endPosition;
    [SerializeField] private Vector3 initDirection, currentDir;
    [SerializeField] private Rigidbody rb;
    private Vector3 initialPosition;
    private bool isActive, isWaiting;
    private WaitForSeconds second = new WaitForSeconds(1);
    private void Start()
    {
        initialPosition = transform.position;
        currentDir = initDirection;
    }

    public void Play()
    {
        StartCoroutine(CoroutineStartElevator(currentDir));
    }

    private void FixedUpdate()
    {
        if (isActive)
        {
            if (Vector3.Distance(transform.position, endPosition.position) < 0.01)
            {
                currentDir = -initDirection;
                StartCoroutine(CoroutineStartElevator(currentDir));
            }
            else if(Vector3.Distance(transform.position, initialPosition) < 0.01)
            {
                currentDir = initDirection;
                StartCoroutine(CoroutineStartElevator(currentDir));
            }
        }
    }

    private IEnumerator CoroutineStartElevator(Vector3 dir)
    {
        isActive = false;
        rb.velocity = Vector3.zero;
        yield return second;
        transform.position += dir / 20;
        rb.velocity = dir;
        isActive = true;
    }

    public void Stop()
    {
        rb.velocity = Vector3.zero;
        isActive = false;
    }
}
