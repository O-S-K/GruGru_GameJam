using System;
using System.Collections.Generic;
using UnityEngine;

namespace OSK
{
    public class PoolManager : SingletonMono<PoolManager>
    {
        public Dictionary<Component, ObjectPool<Component>> prefabLookup = new();
        public Dictionary<Component, ObjectPool<Component>> instanceLookup = new();

        public Transform ContainerPool;
        public bool logStatus;
        private bool dirty = false;

        public void ResetPool()
        {
            foreach (Transform child in ContainerPool)
            {
                Destroy(child.gameObject);
            }

            instanceLookup.Clear();
            prefabLookup.Clear();
        }

        public void WarmPool<T>(T prefab, int size) where T : Component
        {
            if (prefabLookup.ContainsKey(prefab))
            {
                throw new Exception("Pool for prefab " + prefab.name + " has already been created");
            }
            var pool = new ObjectPool<Component>(() =>
            {
                return Instantiate(prefab);
            }, size);
            prefabLookup[prefab] = pool;
            dirty = true;
            //return pool as T;
        }

        #region Spawns 

        public T SpawnObject<T>(T prefab, Vector3 position, Quaternion rotation) where T : Component
        {
            if (!prefabLookup.ContainsKey(prefab))
            {
                WarmPool(prefab, 1);
            }

            var pool = prefabLookup[prefab];
            var clone = pool.GetItem() as T;

            clone.gameObject.SetActive(true);
            clone.transform.SetPositionAndRotation(position, rotation);
            clone.transform.SetParent(ContainerPool);
            instanceLookup.Add(clone, pool);
            dirty = true;
            return clone;
        }

        public T Spawn<T>(T prefab) where T : Component
        {
            if (!prefabLookup.ContainsKey(prefab))
            {
                WarmPool(prefab, 1);
            }

            var pool = prefabLookup[prefab];
            var clone = pool.GetItem() as T;
            clone.gameObject.SetActive(true);
            clone.transform.SetParent(ContainerPool);

            instanceLookup.Add(clone, pool);
            dirty = true;
            return clone;
        }

        public T Spawn<T>(T prefab, Transform parrent) where T : Component
        {
            if (!prefabLookup.ContainsKey(prefab))
            {
                WarmPool(prefab, 1);
            }

            var pool = prefabLookup[prefab];
            var clone = pool.GetItem() as T;
            clone.gameObject.SetActive(true);
            clone.transform.SetParent(parrent);

            instanceLookup.Add(clone, pool);
            dirty = true;
            return clone;
        }


        public void RemoveItemInPool(Component component)
        {
            var pool = prefabLookup[component];
            instanceLookup.Remove(pool.GetItem());
        }
        #endregion

        public void Despawn(Component clone)
        {
            clone.gameObject.SetActive(false);

            if (instanceLookup.ContainsKey(clone))
            {
                instanceLookup[clone].ReleaseItem(clone);
                instanceLookup.Remove(clone);
                dirty = true;
            }
            else
            {
                Debug.LogWarning("No pool contains the object: " + clone.name);
            }
        }



        //#if UNITY_EDITOR
        //        private void Update()
        //        {
        //            if (logStatus && dirty)
        //            {
        //                PrintStatus();
        //                dirty = false;
        //            }
        //        }

        //        private void PrintStatus()
        //        {
        //            foreach (KeyValuePair<Component, ObjectPool<Component>> keyVal in prefabLookup)
        //            {
        //                Debug.Log(string.Format("Object Pool for Prefab: {0} In Use: {1} Total {2}", keyVal.Key.name, keyVal.Value.CountUsedItems, keyVal.Value.Count));
        //            }
        //        }
        //#endif
    }
}


