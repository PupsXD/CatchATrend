using System.Collections.Generic;
using Trends;
using UnityEngine;

namespace Singletones
{
    public class SpawnManager : MonoBehaviour
    {
        public TrendFactory trendFactory;
        public List<Trend> activeTrends;

        [SerializeField] private float spawnInterval = 2f; // Интервал между спавнами
        [SerializeField] private float spawnRadius = 5f; // Радиус области спавна
        [SerializeField] private int maxActiveTrends = 10; // Максимальное количество активных трендов

        private float nextSpawnTime;

        private void Awake()
        {
            trendFactory = GetComponent<TrendFactory>();
            activeTrends = new List<Trend>();
            nextSpawnTime = Time.time + spawnInterval;
        }

        private void Update()
        {
            // Автоматический спавн
            if (Time.time >= nextSpawnTime && activeTrends.Count < maxActiveTrends)
            {
                SpawnTrend(GetRandomSpawnPosition());
                nextSpawnTime = Time.time + spawnInterval;
            }

            // Очистка неактивных трендов из списка
            activeTrends.RemoveAll(trend => trend == null || !trend.gameObject.activeSelf);
        }

        public void SpawnTrend(Vector3 position)
        {
            Trend trend = trendFactory.CreateRandomTrend(position);
            if (trend != null)
            {
                activeTrends.Add(trend);
            }
        }

        private Vector3 GetRandomSpawnPosition()
        {
            Vector2 randomCircle = Random.insideUnitCircle * spawnRadius;
            return transform.position + new Vector3(randomCircle.x, randomCircle.y, 0);
        }
    }
}