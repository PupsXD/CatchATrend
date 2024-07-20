using DefaultNamespace;
using ScriptableObjects;
using UnityEngine;

namespace Trends
{
    public class Trend : MonoBehaviour
    {
        [SerializeField] private TrendData trendData;
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
            _spriteRenderer.enabled = true;
            _spriteRenderer.sprite = trendData.trendSprite;
            _trendName = trendData.trendName;
            trendType = trendData.trendType;
            _rb.mass = trendData.trendMass;
            trendPointsAmount = trendData.trendPointsAmount;
            if (trendData.trendAnimation != null)
                _trendCatchAnimation = trendData.trendAnimationClip();
        }
        
        public void CatchTrend()
        {
            if (_trendCatchAnimation != null)
                GetComponent<Animation>().Play(_trendCatchAnimation.name);
            else
            {
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