using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooler : MonoBehaviour
{
    public static Pooler instance;

    private int i;
    private GameObject objectInstance;
    private Dictionary<String, Pool> pools = new Dictionary<string, Pool>();
    
    [SerializeField] private List<PoolKey> poolKeys = new List<PoolKey>();

    [Serializable]
    public class Pool
    {
        public GameObject prefab;
        public Queue<GameObject> queueDisable = new Queue<GameObject>(); // Liste par probs
        public Queue<GameObject> queueEnable = new Queue<GameObject>(); // Liste par probs
        public int baseCount;
    }

    [Serializable]
    public class PoolKey
    {
        public String key;
        public Pool pool;
    }

    private void Awake()
    {
        instance = this;
        InitPools();
        Populate();
    }
    

    void InitPools()
    {
        for (i = 0; i < poolKeys.Count; i++)
        {
            pools.Add(poolKeys[i].key, poolKeys[i].pool);
        }
    }

    void Populate()
    {
        foreach (var pool in pools)
        {
            PopulatePool(pool.Value);
        }
    }

    void PopulatePool(Pool pool)
    {
        for (i = 0; i < pool.baseCount; i++)
        {
            AddInstance(pool);
        }
    }

    void AddInstance(Pool pool)
    {
        if (pool.prefab == null) return;
        objectInstance = Instantiate(pool.prefab, transform);
        objectInstance.SetActive(false);
        pool.queueDisable.Enqueue(objectInstance);
    }

    public GameObject Pop(string key)
    {
        if (pools[key].queueDisable.Count == 0)
        {
            Debug.LogWarning("pool of "+key +" is empty");
            AddInstance(pools[key]);
        }
        objectInstance = pools[key].queueDisable.Dequeue();
        pools[key].queueEnable.Enqueue(objectInstance);
        objectInstance.SetActive(true);

        return objectInstance;
    }

    public GameObject Pop(string key, Vector3 position)
    {
        if (pools[key].queueDisable.Count == 0)
        {
            Debug.LogWarning("pool of " + key + " is empty");
            AddInstance(pools[key]);
        }
        objectInstance = pools[key].queueDisable.Dequeue();
        pools[key].queueEnable.Enqueue(objectInstance);
        objectInstance.transform.position = position;
        objectInstance.SetActive(true);

        return objectInstance;
    }

    public GameObject Pop(string key, Vector3 position, Quaternion quaternion)
    {
        if (pools[key].queueDisable.Count == 0)
        {
            Debug.LogWarning("pool of " + key + " is empty");
            AddInstance(pools[key]);
        }
        objectInstance = pools[key].queueDisable.Dequeue();
        pools[key].queueEnable.Enqueue(objectInstance);
        objectInstance.transform.position = position;
        objectInstance.transform.rotation = quaternion;
        objectInstance.SetActive(true);

        return objectInstance;
    }

    public GameObject Pop(string key, Transform parent) 
    {
        if (pools[key].queueDisable.Count == 0)
        {
            Debug.LogWarning("pool of "+key +" is empty");
            AddInstance(pools[key]);
        }
        objectInstance = pools[key].queueDisable.Dequeue();
        pools[key].queueEnable.Enqueue(objectInstance);
        objectInstance.transform.parent = parent;
        objectInstance.transform.Rotate(parent.eulerAngles);
        objectInstance.SetActive(true);

        return objectInstance;
    }

    public GameObject Pop(string key, Vector3 position, Transform parent) 
    {
        if (pools[key].queueDisable.Count == 0)
        {
            Debug.LogWarning("pool of "+key +" is empty");
            AddInstance(pools[key]);
        }
        objectInstance = pools[key].queueDisable.Dequeue();
        pools[key].queueEnable.Enqueue(objectInstance);
        objectInstance.transform.position = position;
        objectInstance.transform.parent = parent;
        objectInstance.transform.Rotate(parent.eulerAngles);
        objectInstance.SetActive(true);

        return objectInstance;
    }

    public GameObject Pop(string key, Vector3 position, Quaternion quaternion, Transform parent)
    {
        if (pools[key].queueDisable.Count == 0)
        {
            Debug.LogWarning("pool of " + key + " is empty");
            AddInstance(pools[key]);
        }
        objectInstance = pools[key].queueDisable.Dequeue();
        pools[key].queueEnable.Enqueue(objectInstance);
        objectInstance.transform.position = position;
        objectInstance.transform.parent = parent;
        objectInstance.transform.rotation = quaternion;
        objectInstance.SetActive(true);

        return objectInstance;
    }

    public void DePop(String key, GameObject go)
    {
        pools[key].queueDisable.Enqueue(go);
        pools[key].queueEnable.Dequeue();
        go.transform.parent = transform;
        go.transform.rotation = Quaternion.identity;
        go.SetActive(false);
    }

    public void DelayedDePop(float t, string key, GameObject go)
    {
        StartCoroutine(DelayedDePopCoroutine(t, key, go));
    }

    IEnumerator DelayedDePopCoroutine(float t, string key, GameObject go)
    {
        yield return new WaitForSeconds(t);
        DePop(key, go);
    }
}