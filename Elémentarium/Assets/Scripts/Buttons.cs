using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Buttons : MonoBehaviour
{
    [SerializeField] private UnityEvent onPressed, onReleased;
    [SerializeField] private ConfigurableJoint _configurableJoint;
    [SerializeField] private float deadZone, treshold;
    private bool _isPressed;
    private Vector3 _starPos;
    private float distance;
    
    // Start is called before the first frame update
    void Start()
    {
        _starPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(GetValue());
        if (!_isPressed && GetValue() + treshold >= 1)
        {
            Pressed();
        }

        if (_isPressed && GetValue() - treshold <= 0)
        {
            Release();
        }
    }

    float GetValue()
    {
        distance = Vector3.Distance(_starPos, transform.localPosition) / _configurableJoint.linearLimit.limit;
        
        /*if (Mathf.Abs(distance) < deadZone)
        {
            return 0;
        }*/

        return Mathf.Clamp(distance,-1,1);
    }

    void Pressed()
    {
        _isPressed = true;
        onPressed.Invoke();
        Debug.Log("Pressed");
    }
    
    void Release()
    {
        _isPressed = false;
        onReleased.Invoke();
        Debug.Log("Pressed");
    }
}
