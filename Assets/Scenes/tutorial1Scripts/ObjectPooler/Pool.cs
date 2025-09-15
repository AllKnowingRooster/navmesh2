using System.Collections.Generic;
using UnityEngine;

public class Pool 
{

    private List<PoolableObject> listPoolableObject;
    private int size;
    public PoolableObject poolableObjectPrefab;

    private Pool(PoolableObject prefab,int size)
    {
        this.size = size;
        this.poolableObjectPrefab = prefab;
        this.listPoolableObject = new List<PoolableObject>(size);
    }

    public static Pool CreatePoolInstance(PoolableObject prefab,int size)
    {
        Pool instancePool=new Pool(prefab,size);
        GameObject gameObjectPool = new GameObject(prefab + " Pool");
        instancePool.GenerateGameObject(gameObjectPool);
        return instancePool;
    }

    private void GenerateGameObject(GameObject parent)
    {
        for (int i=0;i<size;i++)
        {
            PoolableObject poolableObjectGameObject = GameObject.Instantiate(poolableObjectPrefab,Vector3.zero,poolableObjectPrefab.transform.rotation,parent.transform);
            poolableObjectGameObject.ObjectPool= this;
            poolableObjectGameObject.gameObject.SetActive(false);
        }
    }

    public PoolableObject GetObject()
    {
        if (listPoolableObject.Count==0)
        {
            return null;
        }
        PoolableObject poolableGameObject = listPoolableObject[0];
        listPoolableObject.RemoveAt(0);
        poolableGameObject.gameObject.SetActive(true);
        return poolableGameObject;
    }

    public void ReturnToPool(PoolableObject gameObject)
    {
        listPoolableObject.Add(gameObject);
    }

}
