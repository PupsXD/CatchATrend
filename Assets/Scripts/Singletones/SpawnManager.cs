using System.Collections.Generic;
using Trends;
using UnityEngine;

namespace Singletones
{
    public class SpawnManager : MonoBehaviour
    {
        public TrendFactory trendFactory;
        public List<Trend> activeTrends;
        
        private void Awake()
        {
            trendFactory = GetComponent<TrendFactory>();
            activeTrends = new List<Trend>();
        }
        
        public void SpawnTrend(Vector3 position)
        {
            Trend trend = trendFactory.CreateRandomTrend(position);
            if (trend != null)
            {
                activeTrends.Add(trend);
            }
        }
        
        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                SpawnTrend(transform.position);
            }
        }
        
        
        
    }
}