using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateRoadState : State
{
    private RoadBehaviour roadBehaviour;
    private BoxCollider boxCollider;
    private float time = 0f;
    private Transform rotatePoint;

    public RotateRoadState(PlayerBehaviour player, RoadBehaviour roadBehaviour, Transform rotatePoint) : base(player)
    {
        this.roadBehaviour = roadBehaviour;
        this.rotatePoint = rotatePoint;
    }

    public override void Tick()
    {
        boxCollider.size = new Vector3(1f, 1f, 1f);
        
        time += Time.deltaTime;
        var rotationAngle = time * (90 / player.GetVelocityPlatformDown());
        
        if (rotationAngle <= 90)
        {
            rotatePoint.rotation = Quaternion.Euler(rotationAngle, 0f, 0f);            
        }
        else{
            player.SetState(new MoveToEndOfRoadState(player, roadBehaviour.GetEndOfRoad()));
        }        
    }

    public override void OnStateEnter()
    {
        boxCollider = roadBehaviour.GetComponent<BoxCollider>();
    }
}
