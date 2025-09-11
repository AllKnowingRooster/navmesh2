using UnityEngine;

public class PoolableObject : MonoBehaviour
{
    public Pool ObjectPool;
    
    public virtual void OnDisable()
    {
        ObjectPool.ReturnToPool(this);
    }

}
