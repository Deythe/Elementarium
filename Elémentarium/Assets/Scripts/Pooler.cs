using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        public Queue<GameObject> queue = new Queue<GameObject>();
        public int baseCount;
        public float baseRefreshSpeed = 5;
        public float refreshSpeed = 5;
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
        objectInstance = Instantiate(pool.prefab, transform);
        objectInstance.SetActive(false);
        
        pool.queue.Enqueue(objectInstance);
    }

    void Start()
    {
        InitRefreshCount();
    }

    void InitRefreshCount()
    {
        foreach (KeyValuePair<String, Pool> pool in pools)
        {
            StartCoroutine(RefreshPool(pool.Value, pool.Value.baseRefreshSpeed));
        }
    }

    IEnumerator RefreshPool(Pool pool, float t)
    {
        yield return new WaitForSeconds(t);
        if (pool.queue.Count < pool.baseCount)
        {
            AddInstance(pool);
            pool.refreshSpeed = pool.baseRefreshSpeed * pool.queue.Count / pool.baseCount;
        }
        
        StartCoroutine(RefreshPool(pool, pool.refreshSpeed));
    }
    
    public GameObject Pop(string key)
    {
        if (pools[key].queue.Count == 0)
        {
            Debug.LogWarning("pool of "+key +" is empty");
            AddInstance(pools[key]);
        }

        objectInstance = pools[key].queue.Dequeue();
        objectInstance.SetActive(true);

        return objectInstance;
    }

    public GameObject Pop(string key, Vector3 position)
    {
        if (pools[key].queue.Count == 0)
        {
            Debug.LogWarning("pool of " + key + " is empty");
            AddInstance(pools[key]);
        }

        objectInstance = pools[key].queue.Dequeue();
        objectInstance.transform.position = position;
        objectInstance.SetActive(true);

        return objectInstance;
    }

    public GameObject Pop(string key, Transform parent)
    {
        if (pools[key].queue.Count == 0)
        {
            Debug.LogWarning("pool of " + key + " is empty");
            AddInstance(pools[key]);
        }

        objectInstance = pools[key].queue.Dequeue();
        objectInstance.transform.parent = parent;
        objectInstance.SetActive(true);

        return objectInstance;
    }

    public GameObject Pop(string key, Vector3 position, Transform parent)
    {
        if (pools[key].queue.Count == 0)
        {
            Debug.LogWarning("pool of " + key + " is empty");
            AddInstance(pools[key]);
        }

        objectInstance = pools[key].queue.Dequeue();
        objectInstance.transform.position = position;
        objectInstance.transform.parent = parent;
        objectInstance.SetActive(true);

        return objectInstance;
    }

    public void DePop(String key, GameObject go)
    {
        pools[key].queue.Enqueue(go);
        go.transform.parent = transform;
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