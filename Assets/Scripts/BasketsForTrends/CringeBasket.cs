using System.Collections.Generic;
using DefaultNamespace;
using Trends;
using UnityEngine;

namespace BasketsForTrends
{
    public class CringeBasket : Basket
    {
        public List<Trend> cringeTrends;

        private void Awake()
        {
            cringeTrends = new List<Trend>();
        }
        public override void AcceptTrends(Trend trend)
        {
            if (trend.trendType == TrendType.Cringe)
            {
                cringeTrends.Add(trend);
            }
        }
    }
}