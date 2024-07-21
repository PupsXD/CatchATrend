using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Trend Data", menuName = "Create Trend/Trend Data", order = 0)]
    public class TrendData : ScriptableObject
    {
        public Sprite trendSprite;
        public bool isHype;
        public int trendMass;
        public string trendName;
        public TrendType trendType;
        public float trendPointsAmount;
        public TrendVisualType trendVisualType;
        public TrendAnimation trendAnimation;

        public AnimationClip GetTrendAnimationClip()
        {
            return trendAnimation?.GetAnimationForType(trendVisualType);
        }

        
 
    }
}