using DefaultNamespace;
using ScriptableObjects;
using UnityEngine;

namespace Trends
{
    public class Trend : MonoBehaviour
    {
        [SerializeField] private TrendData trendData;
        [SerializeField] private SpriteRenderer animationSpriteRenderer;
        public Animator finishAnimator;
        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _rb;
        private string _trendName;
        private AnimationClip _trendCatchAnimation;
        
        
        public float trendPointsAmount { get; private set; }

        public TrendType trendType;
        


        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _rb = GetComponent<Rigidbody2D>();
        }

        public void Initialize(TrendData trendData)
        {
            animationSpriteRenderer.enabled = false;
            _spriteRenderer.enabled = true;
            _spriteRenderer.sprite = trendData.trendSprite;
            _trendName = trendData.trendName;
            trendType = trendData.trendType;
            _rb.mass = trendData.trendMass;
            trendPointsAmount = trendData.trendPointsAmount;
            if (trendData.trendAnimation != null)
            {
                _trendCatchAnimation = trendData.GetTrendAnimationClip();
                Debug.Log($"Trend {gameObject.name} initialized with animation: {_trendCatchAnimation?.name ?? "null"}");
            }

        }
        
        public void CatchTrend()
        {
            if (_trendCatchAnimation != null)
            {
                // Используем Animator вместо Animation
                animationSpriteRenderer.enabled = true;
                finishAnimator.Play(_trendCatchAnimation.name);
            }
            else
            {
                animationSpriteRenderer.enabled = true;
                finishAnimator.SetBool("isDestroyed", true);
            }
        }

        public void SwitchImageActive()
        {
            if (_spriteRenderer != null)
            {
                _spriteRenderer.enabled = !_spriteRenderer.enabled;
            }
            
        }
        
    }
}