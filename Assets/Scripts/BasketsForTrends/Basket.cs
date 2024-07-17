using System.Collections.Generic;
using Trends;
using UnityEngine;

namespace BasketsForTrends
{
    public abstract class Basket : MonoBehaviour
    {
        public Trend CurrentTrend;
        public List<Trend> accseptedTrend = new List<Trend>();
        
        public virtual void AcceptTrends(Trend trend)
        {
            
        }
    }
}