using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
    ObjectPool
        This script is used to create a pool of objects that can be used in the game.
        This script is attached to an empty game object in the scene.
        Setup:
            The pool size is set in the inspector.
            The prefab object is set in the inspector.
            The pool is filled in the Awake method.
        Methods:
            The Withdraw method is used to get an object from the pool.
            The Deposit method is used to return an object to the pool.
 */

public class ObjectPool : MonoBehaviour
{
    public int poolSize = 100;
    public GameObject prefabObject;
    private Queue<GameObject> pool;

    private void Awake()
    {
        pool = new Queue<GameObject>();
        Fill();
    }

    public void Fill()
    {
        if (prefabObject == null) return;
        GameObject tempGameObject;
        while (pool.Count < poolSize)
        {
            tempGameObject = Instantiate(prefabObject, transform.position, Quaternion.identity, transform);
            pool.Enqueue(tempGameObject);
            tempGameObject.SetActive(false);
        }
    }

    public GameObject Withdraw()
    {
        if (pool.Count < poolSize / 4)
            Fill();

        GameObject tempGameObject = pool.Dequeue();
        tempGameObject.SetActive(true);
        return tempGameObject;
    }

    public void Deposit(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.parent = transform;
        pool.Enqueue(obj);
    }
}
