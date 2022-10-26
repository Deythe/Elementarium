using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;


public class Expulse : MonoBehaviour
{
    [SerializeField] private InputActionProperty triggerAction;
    [SerializeField] private Transform spawnPoint; 
    [SerializeField] private GameObject bulletPrefabs;
    [SerializeField] private int force;
    [SerializeField] private float bloom;

    [Range(0.01f, 1f)]
    [SerializeField] private float cooldownMin;
    [Range(0.01f, 1f)]
    [SerializeField] private float cooldownMax;
    private float cooldown;
    private bool canFire = true;

    private GameObject bullet;

    private void Start()
    {
        cooldown = UnityEngine.Random.Range(cooldownMin, cooldownMax);
    }

    public void Update()
    {
        if (canFire && triggerAction.action.ReadValue<float>() > 0.1f)
        {
            ShootBullet();
        }
    }

    public void ShootBullet()
    {
        bullet = Pooler.instance.Pop("Water");
        bullet.GetComponent<Rigidbody>().velocity = Vector3.zero;
        bullet.transform.position = spawnPoint.transform.position;
        bullet.GetComponent<Rigidbody>().AddForce((transform.forward + transform.right * UnityEngine.Random.Range(-bloom, bloom) + transform.up * UnityEngine.Random.Range(-bloom, bloom)).normalized * force, ForceMode.Impulse);
        StartCoroutine(CooldownCoroutine(cooldown));
    }

    IEnumerator CooldownCoroutine(float t) 
    {
        canFire = false;

        yield return new WaitForSeconds(t);

        cooldown = UnityEngine.Random.Range(cooldownMin, cooldownMax);
        canFire = true;
    }
}
