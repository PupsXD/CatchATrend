using System.Collections.Generic;
using DefaultNamespace;
using Trends;
using UnityEngine;

namespace BasketsForTrends
{
    public class HypeBasket : Basket
    {
        private void Awake()
        {
            accseptedTrend = new List<Trend>();
        }
        public override void AcceptTrends(Trend trend)
        {
            if (trend.trendType == TrendType.Hype)
            {
                accseptedTrend.Add(trend);
            }
        }
    }
}