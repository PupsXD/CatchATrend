using System;
using BasketsForTrends;
using Singletones;
using UnityEngine;

namespace Trends
{
    public class TrendBasketBehaviour : MonoBehaviour
    {
        private Rigidbody2D _rb;
        private Trend _trend;
        private TrendPool _trendPool;
        private string _tag;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _trend = GetComponent<Trend>();
        }

        public void Initialize(TrendPool pool, string tag)
        {
            _trendPool = pool;
            _tag = tag;
            _rb.simulated = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<Basket>() != null)
            {
                other.gameObject.GetComponent<Basket>().AcceptTrends(_trend);
                ReturnToPool();
                
            }
        }

        private void ReturnToPool()
        {
            _trend.CatchTrend();
            _rb.simulated = false;
            _trendPool.ReturnToPool(_tag, gameObject);
        }
    }
}