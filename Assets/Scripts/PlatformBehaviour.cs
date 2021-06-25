using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBehaviour : MonoBehaviour
{
    [SerializeField] private Transform roadSpawn;
    [SerializeField] private RoadBehaviour road;
    [SerializeField] private bool endGame = false;
    

    public RoadBehaviour InstantiateRoad()
    {
        return Instantiate(road, roadSpawn.transform.position, Quaternion.identity, roadSpawn);
    }

    public Transform GetRoadPoint() => roadSpawn.transform;

    public bool IsEndGame => endGame;    
}
