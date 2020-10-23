using System.Collections.Generic;
using UnityEngine;
using System;

public class Pool<T> where T : PoolBehaviour
{
    private List<T> _objectPool = new List<T>();
    private Func<T> _create;
    private T _firstInactive;

    public Pool(Func<T> creationFunction, int count)
    {
        _create = creationFunction;

        for (int i = 0; i < count; i++)
        {
            var poolObject = _create.Invoke();
            poolObject.ReturnToPool = ReturnObject;
            _objectPool.Add(poolObject);
        }

        for (int i = 0; i < count - 1; i++)
            _objectPool[i].NextInactive = _objectPool[i + 1];

        _firstInactive = _objectPool[0];
    }
    
    public T GetObject()
    {
        if (_firstInactive != null)
        {
            var poolObject = (T)_firstInactive;
            _firstInactive = (T)_firstInactive.NextInactive;
            return poolObject;
        }
        else
        {
            T poolObject = _create.Invoke();
            poolObject.ReturnToPool = ReturnObject;
            _objectPool.Add(poolObject);
            return poolObject;
        }
    }

    private void ReturnObject(PoolBehaviour poolObject)
    {
        poolObject.Reset();
        poolObject.NextInactive = _firstInactive;
        _firstInactive = (T)poolObject;
    }
}