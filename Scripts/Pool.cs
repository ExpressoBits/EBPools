﻿using UnityEngine;
using System.Collections.Generic;
using System;


namespace ExpressoBits.PoolSimply
{

    /**
     * Class that manages the pool dictionary 
     * This class is static and is used by the MonoBehaviour Instantiate and Destroy method extenders
     * 
     **/
    [AddComponentMenu("Pool/Pool")]
    public class Pool : MonoBehaviour
    {
        [HideInInspector]
        public Queue<GameObject> objects  = new Queue<GameObject>();
        //[SerializeField]
        //private GameObject prefab;
        [SerializeField]
        public PoolData poolData;

        private void Awake() {
            //InstantiateAmount(objects,prefab,poolData.initialIncrease);
        }

        private void Start() {
            Pools.Instance().RegisterPool(poolData,this);
        }

        private void OnDestroy() {
            Clear();
            Pools.Instance().Unregister(poolData);
        }

        #region EnqueueAndDequeue
        /**
         * Add to queue prefab and set object disabled
         **/
        public void Enqueue(GameObject obj)
        {
            OnPoolerDisable(obj);
            objects.Enqueue(obj);
            
        }

        public GameObject Instantiate(GameObject prefab){
            return Dequeue(prefab);
        }

        /**
         * Get object from queue with prefab model, if no exist
         **/
        public GameObject Dequeue(GameObject prefab)
        {
            GameObject obj;
            //TODO Make this more efficiely
            if (objects.Count == 0){
                InstantiateAmount(objects,prefab,poolData.increaseAmount);
            }
            obj = objects.Dequeue();
            if(obj == null){
                GameObject.Instantiate(prefab);
            }
            OnPoolerEnable(obj);
            return obj;
        }
        
        public GameObject Dequeue(GameObject prefab,Vector3 position, Quaternion rotation)
        {
            GameObject obj = Dequeue(prefab);
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            return obj;
        }
        #endregion
        
        /**
         * Instance amount gameobjects in queue first params
         **/
        private void InstantiateAmount(Queue<GameObject> objects, GameObject prefab, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                GameObject gameObject = GameObject.Instantiate(prefab);
                gameObject.SetActive(false);
                objects.Enqueue(gameObject);
            }
        }

        #region Utils
        public void Clear()
        {
            foreach (GameObject obj in objects)
            {
                GameObject.Destroy(obj);
            }
            objects.Clear();
        }

        

        public void IncreaseAmount(GameObject prefab){
            InstantiateAmount(objects,prefab,poolData.increaseAmount);
        }
        
        public void OnPoolerEnable(GameObject obj)
        {
            obj.SetActive(true);
            foreach (IPooler ipooler in obj.GetComponents<IPooler>())
            {
                ipooler.OnPoolerEnable();
            }
        }

        public void OnPoolerDisable(GameObject obj)
        {
            obj.SetActive(false);
            foreach (IPooler ipooler in obj.GetComponents<IPooler>())
            {
                ipooler.OnPoolerDisable();
            }
        }

        #endregion
    }
}
