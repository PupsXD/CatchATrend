using DefaultNamespace;
using ScriptableObjects;
using UnityEngine;

namespace Trends
{
    public class Trend : MonoBehaviour
    {
        [SerializeField] private TrendData trendData;
        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _rb;
        private string _trendName;
        private AnimationClip _trendCatchAnimation;
        
        //public TrendVisualType trendVisualType { get; private set; }
        public float trendPointsAmount { get; private set; }

        public TrendType trendType;
        
//public bool isHype { get; private set; }

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _rb = GetComponent<Rigidbody2D>();
        }

        public void Initialize(TrendData trendData)
        {
            _spriteRenderer.sprite = trendData.trendSprite;
            _trendName = trendData.trendName;
            trendType = trendData.trendType;
            //isHype = trendData.isHype;
            _rb.mass = trendData.trendMass;
            trendPointsAmount = trendData.trendPointsAmount;
            _trendCatchAnimation = trendData.trendAnimationClip();
            //trendVisualType = trendData.trendVisualType;
        }
    }
}