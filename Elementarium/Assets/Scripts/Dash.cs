using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dash : MonoBehaviour
{
    [SerializeField] private InputActionProperty _rightTp;
    [SerializeField] private HandController _rightHand;
    [SerializeField] private CharacterController _cc;
    
    private Vector3 _positionTarget, _offset;
    private bool _isPressed;
    private Coroutine currentCoroutine;

    private void Update()
    {
        if (_rightTp.action.WasPressedThisFrame() && !_rightTp.action.ReadValue<Vector2>().magnitude.Equals(0))
        {
            _isPressed = true;
        }

        if (_rightTp.action.WasReleasedThisFrame() && _isPressed)
        {
            _isPressed = false;
            _rightHand.rayHand.TryGetHitInfo(out _positionTarget, out _, out _, out _);
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }
            
            currentCoroutine = StartCoroutine(CoroutineDash());
        }
        
    }

    IEnumerator CoroutineDash()
    {
        float timer = 0;
        do
        {
            timer += 0.01f;
            _cc.transform.position = Vector3.Lerp(_cc.transform.position, new Vector3(_positionTarget.x, _cc.transform.position.y, _positionTarget.z), timer);
            yield return new WaitForFixedUpdate();
        } while (timer < 1f);
    }
}
