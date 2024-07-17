using DefaultNamespace;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Trend Data", menuName = "Create Trend", order = 0)]
    public class TrendData : ScriptableObject
    {
        public Sprite trendSprite;
        public bool isHype;
        public int trendMass;
        public string trendName;
        public TrendType trendType;
    }
}