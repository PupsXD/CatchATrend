using DefaultNamespace;
using Trends;

namespace BasketsForTrends
{
    public class LostTrendTrigger: Basket
    {
        public override void AcceptTrends(Trend trend)
        {
            accseptedTrend.Add(trend);
            
            
        }
    }
}