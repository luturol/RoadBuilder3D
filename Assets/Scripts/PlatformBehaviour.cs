using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBehaviour : MonoBehaviour
{
    [SerializeField] private Transform roadSpawn;
    [SerializeField] private RoadBehaviour road;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public RoadBehaviour InstantiateRoad()
    {
        return Instantiate(road, roadSpawn.transform.position, Quaternion.identity, roadSpawn);
    }

    public Transform GetRoadPoint() => roadSpawn.transform;
    
}
