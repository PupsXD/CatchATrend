using System.Collections.Generic;
using UnityEngine;

namespace Singletones
{
    
    public class TrendPool : MonoBehaviour
    {
        [System.Serializable]
        public class Pool
        {
            public GameObject trendPrefab;
            public int size;
            public string tag;
        }

        public List<Pool> pools;
        public Dictionary<string, Queue<GameObject>> poolDictionary;

        private void Start()
        {
            poolDictionary = new Dictionary<string, Queue<GameObject>>();
            foreach (Pool pool in pools)
            {
                Queue<GameObject> objectPool = new Queue<GameObject>();
                for (int i = 0; i < pool.size; i++)
                {
                    GameObject obj = Instantiate(pool.trendPrefab);
                    obj.SetActive(false);
                    objectPool.Enqueue(obj);
                }

                poolDictionary.Add(pool.tag, objectPool);

            }
        }
        
        public GameObject SpawnTrend(Vector3 position, string tag)
        {
            if (!poolDictionary.ContainsKey(tag))
            {
                Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
                return null;
            }
            
            GameObject trend = poolDictionary[tag].Dequeue();
            trend.SetActive(true);
            trend.transform.position = position;
            return trend;
            //poolDictionary[tag].Enqueue(trend); комментируем, потому что объект может быть всё ещё активен в сцене

        }


        public void ReturnToPool(string tag, GameObject deactivatedTrand)
        {
            if (!poolDictionary.ContainsKey(tag))
            {
                Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
                return;
            }
            poolDictionary[tag].Enqueue(deactivatedTrand);
            deactivatedTrand.SetActive(false);
            
        }
    }
}