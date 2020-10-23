using UnityEngine;
using System;

public abstract class PoolBehaviour : MonoBehaviour
{
    public PoolBehaviour NextInactive { get; set; }
    public Action<PoolBehaviour> ReturnToPool { get; set; }
    
    public virtual void Free() 
    {
        ReturnToPool.Invoke(this);
    }
    
    public virtual void Reset() 
    {

    }
    
}