using System;
using System.Collections.Generic;
using Game.Scripts.Managers;
using UnityEngine;

namespace Game.Scripts.Patterns
{
    public class ObjectPooler : MonoSingleton<ObjectPooler>
    {
        public List<Pool> pools;
        private Dictionary<PlayerStates, Queue<GameObject>> poolDictionary;

        private void Awake()
        {
            poolDictionary = new Dictionary<PlayerStates, Queue<GameObject>>();
            foreach (var pool in pools)
            {
                var objectPool = new Queue<GameObject>();
                for (var i = 0; i < pool.size; i++)
                {
                    var obj = Instantiate(pool.prefab);
                    obj.SetActive(false);
                    objectPool.Enqueue(obj);
                }

                poolDictionary.Add(pool.state, objectPool);
            }
        }

        public GameObject SpawnFromPool(PlayerStates state, Vector3 position, Quaternion rotation)
        {
            if (!poolDictionary.ContainsKey(state))
            {
                Debug.Log("Pool with tag" + state + "doesnt exist");
                return null;
            }

            var objToSpawn = poolDictionary[state].Dequeue();
            objToSpawn.SetActive(true);
            objToSpawn.transform.position = position;
            objToSpawn.transform.rotation = rotation;

            poolDictionary[state].Enqueue(objToSpawn);
            return objToSpawn;
        }
    }

    [Serializable]
    public class Pool
    {
        public PlayerStates state;
        public GameObject prefab;
        public int size;
    }

    [Serializable]
    public enum PlayerStates
    {
        Old,
        Slow,
        Average,
        Modern,
        Futuristic
    }
}