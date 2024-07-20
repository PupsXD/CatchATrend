using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Trend Animation", menuName = "Create Trend/Animation", order = 0)]
    public class TrendAnimation : ScriptableObject
    {
        public List<TrendVisualPair> trendVisuals;
        [System.Serializable]
        public class TrendVisualPair
        {
            public TrendVisualType visualType;
            public AnimationClip animation;
        }
        
        public AnimationClip GetAnimationForType(TrendVisualType type)
        {
            return trendVisuals.Find(pair => pair.visualType == type)?.animation;
        }
        
        
    }
}