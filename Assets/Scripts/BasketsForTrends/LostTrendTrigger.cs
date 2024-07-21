using DefaultNamespace;
using Trends;
using UnityEngine;

namespace BasketsForTrends
{
    public class LostTrendTrigger: Basket
    {
        [SerializeField] private TrendMeter trendMeter;
        public override void AcceptTrends(Trend trend)
        {
            accseptedTrend.Add(trend);
            trendMeter.AddCringe(trend.trendPointsAmount);
            
            
        }
    }
}