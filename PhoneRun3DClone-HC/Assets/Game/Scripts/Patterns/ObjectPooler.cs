using System;
using System.Collections.Generic;
using Game.Scripts.Managers;
using UnityEngine;

namespace Game.Scripts.Patterns
{
    public class ObjectPooler : MonoSingleton<ObjectPooler>
    {
        public List<Pool> pools;
        public Dictionary<PlayerStates, Queue<GameObject>> poolDictionary;
        private void Awake()
        {
            
            poolDictionary = new Dictionary<PlayerStates, Queue<GameObject>>();
            foreach (var pool in pools)
            {
                Queue<GameObject> objectPool = new Queue<GameObject>();
                for (int i = 0; i < pool.size; i++)
                {
                    GameObject obj = Instantiate(pool.prefab);
                    obj.SetActive(false);
                    objectPool.Enqueue(obj);
                }

                poolDictionary.Add(pool.state, objectPool);
            }
        }

        public GameObject SpawnFromPool(PlayerStates tag, Vector3 position, Quaternion rotation)
        {
            if (!poolDictionary.ContainsKey(tag))
            {
                Debug.Log("Pool with tag" + tag + "doesnt exist");
                return null;
            }

            GameObject objToSpawn = poolDictionary[tag].Dequeue();
            objToSpawn.SetActive(true);
            objToSpawn.transform.position = position;
            objToSpawn.transform.rotation = rotation;

            poolDictionary[tag].Enqueue(objToSpawn);
            return objToSpawn;
        }
    }

    [System.Serializable]
    public class Pool
    {
        public PlayerStates state;
        public GameObject prefab;
        public int size;
    }
    /*[System.Serializable]
    public class Player
    {
        public PlayerStates state;
        public GameObject prefab;
    }*/
    [System.Serializable]
    public enum PlayerStates
    {
        Old,
        Slow,
        Average,
        Modern,
        Futuristic
    }
}