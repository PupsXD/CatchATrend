using System;
using System.Collections;
using BasketsForTrends;
using Singletones;
using UnityEngine;

namespace Trends
{
    public class TrendBasketBehaviour : MonoBehaviour
    {
        [SerializeField] private float returnToPoolDelay = 1f;
        private Rigidbody2D _rb;
        private Trend _trend;
        private TrendPool _trendPool;
        private string _tag;
        private bool _isReturning = false;

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
                StartCoroutine(ReturnToPoolWithDelay());

            }
        }

        private void ReturnToPool()
        {
            _trend.CatchTrend();
            _rb.simulated = false;
            _trendPool.ReturnToPool(_tag, gameObject);
        }
        private IEnumerator ReturnToPoolWithDelay()
        {
            _isReturning = true;
            _rb.simulated = false;
            _trend.SwitchImageActive();
            _trend.CatchTrend();

            // Ждем, пока анимация не закончится
            if (_trend.finishAnimator != null)
            {
                yield return new WaitForSeconds(GetAnimationLength("Destroyed"));
            }
            else
            {
                yield return new WaitForSeconds(returnToPoolDelay);
            }

            _trendPool.ReturnToPool(_tag, gameObject);
        }

        private float GetAnimationLength(string animationName)
        {
            AnimationClip[] clips = _trend.finishAnimator.runtimeAnimatorController.animationClips;
            foreach (AnimationClip clip in clips)
            {
                if (clip.name == animationName)
                {
                    return clip.length;
                }
            }
            return returnToPoolDelay; // Возвращаем стандартную задержку, если анимация не найдена
        }
    }
}
