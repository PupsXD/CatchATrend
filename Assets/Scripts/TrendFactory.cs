using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using ScriptableObjects;
using Singletones;
using Trends;
using UnityEngine;
using Random = UnityEngine.Random;

public class TrendFactory : MonoBehaviour
{
    [SerializeField] private GameObject trendPrefab;
    [SerializeField] private List<TrendData> availableTrends;
    [SerializeField] private TrendPool trendPool;
    
    public class TagOfTrend
    {
        public const string Cringe = "Cringe";
        public const string Hype = "Hype";
    }
    
    public Trend CreateTrend(string trendName, Vector3 position)
    {
        TrendData trendData = availableTrends.Find(trend => trend.trendName == trendName);
        if (trendData == null)
        {
            Debug.LogError("Trend data not found");
            return null;
        }
        
        //GameObject trendObject = Instantiate(trendPrefab, position, Quaternion.identity);
        string tag = TagTypeOfTrend(trendData.trendType);
        Debug.Log(tag);
        GameObject trendObject = trendPool.SpawnTrend(transform.position, tag);

        if (trendObject == null)
        {
            Debug.LogError("Trend object is not assigned in factory");
            return null;
        }
        
        
        Trend trend = trendObject.GetComponent<Trend>();

        if (trend == null)
        {
            Debug.LogError("Trend is not assigned in factory and can not be initialized");
            return null;
        }
        trend.Initialize(trendData);
        TrendBasketBehaviour trendBasketBehaviour = trendObject.GetComponent<TrendBasketBehaviour>();
        if (trendBasketBehaviour == null)
        {
            Debug.LogError("Trend Behavour in basket is not properly initialized");
            return null;
        }
        trendBasketBehaviour.Initialize(trendPool, tag);
        return trend;
    }
    
    public Trend CreateRandomTrend(Vector3 position)
    {
        if (availableTrends.Count == 0)
        {
            Debug.LogError("No available trends!");
            return null;
        }

        TrendData randomData = availableTrends[Random.Range(0, availableTrends.Count)];
        return CreateTrend(randomData.trendName, position);
    }

    private string TagTypeOfTrend(TrendType typeOfTrend)
    {
        switch (typeOfTrend)
        {
            case TrendType.Cringe:
                return TagOfTrend.Cringe;
            case TrendType.Hype:
                return TagOfTrend.Hype;
            default:
                throw new ArgumentException("Unknown trend type");
        }
    }
}
