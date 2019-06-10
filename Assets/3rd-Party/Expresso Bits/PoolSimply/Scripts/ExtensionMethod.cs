﻿using UnityEngine;
using System;
using ExpressoBits.PoolSimply;

public static class ExtensionMethods
{
    public static GameObject InstantiateInPool(this MonoBehaviour value, GameObject gameObject)
    {
        return Pools.Instance().Instantiate(gameObject);
    }

    public static GameObject InstantiateInPool(this MonoBehaviour value, GameObject gameObject, 
    Vector3 position, Quaternion rotation)
    {
        return Pools.Instance().Instantiate(gameObject,position,rotation);
    }

    public static void DestroyInPool(this MonoBehaviour value, GameObject gameObject)
    {
        Pools.Instance().Destroy(gameObject);
    }

}

