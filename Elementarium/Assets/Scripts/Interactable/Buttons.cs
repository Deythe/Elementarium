using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Buttons : MonoBehaviour
{
    [SerializeField] private UnityEvent onPressed, onReleased;
    [SerializeField] private float treshold;
    [SerializeField] private Transform _transformFinalPosition;
    [SerializeField] private Material matInit, matChanged;
    private bool _isPressed;
    private Vector3 _starPos, lastPosition, maxPosition, minPosition;
    private float distance;
    
    // Start is called before the first frame update
    void Start()
    {
        _starPos = transform.localPosition;
        lastPosition = _transformFinalPosition.localPosition;

        if (_starPos.magnitude > lastPosition.magnitude)
        {
            maxPosition = _starPos;
            minPosition = lastPosition;
        }
        else
        {
            maxPosition = lastPosition;
            minPosition = _starPos;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isPressed && GetValue() + treshold >= 1)
        {
            Pressed();
        }

        if (_isPressed && GetValue() - treshold <= 0)
        {
            Release();
        }

        if (transform.localPosition.magnitude > maxPosition.magnitude)
        {
            transform.localPosition = maxPosition;
        }
        
        if (transform.localPosition.magnitude < minPosition.magnitude)
        {
            transform.localPosition = minPosition;
        }
        
    }

    float GetValue()
    {
        distance = 1 - Mathf.Abs(Vector3.Distance(transform.localPosition, lastPosition)) / Mathf.Abs(Vector3.Distance(_starPos, lastPosition));
        return distance;
    }

    void Pressed()
    {
        _isPressed = true;
        onPressed.Invoke();
    }
    
    void Release()
    {
        _isPressed = false;
        onReleased.Invoke();
        Debug.Log("Pressed");
    }

    public void ChangeColor(bool b)
    {
        if (b)
        {
            GetComponent<MeshRenderer>().material = matChanged;
            return;
        }
        
        GetComponent<MeshRenderer>().material = matInit;
    }
}
