using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GiveGloves : MonoBehaviour
{
    [SerializeField] private UnityEvent takeEvent;

    public void GivesGloves()
    {
        PlayerManager.instance.GiveGlove();
        takeEvent?.Invoke();
        Destroy(gameObject);
    }
}
