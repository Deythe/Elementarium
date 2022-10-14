using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class FireBullet : MonoBehaviour
{
    [SerializeField] private InputActionProperty triggerAction, gripAction;
    [SerializeField] private Transform spawnPoint; 
    [SerializeField] private GameObject bulletPrefabs;
    [SerializeField] private int force;
    private GameObject bullet;

    public void Update()
    {
        if (triggerAction.action.ReadValue<float>() > 0.1f)
        {
            ShootBullet();
        }
    }

    public void ShootBullet()
    {
        bullet = Instantiate(bulletPrefabs);
        bullet.transform.position = spawnPoint.transform.position;
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * force, ForceMode.Impulse);
    }
}
