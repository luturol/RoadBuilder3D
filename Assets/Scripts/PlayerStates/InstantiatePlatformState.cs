using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatePlatformState : State
{
    private PlatformBehaviour platformBehaviour;

    private bool firstClickSpace = false;
    private bool hasReleasedSpacebar = false;
    private RoadBehaviour roadPrefab;

    public InstantiatePlatformState(PlayerBehaviour player, PlatformBehaviour platformBehaviour) : base(player)
    {
        this.platformBehaviour = platformBehaviour;
    }

    public override void Tick()
    {
        bool isSpacePressed = Input.GetKey(KeyCode.Space);        
        if (isSpacePressed)
        {
            if (firstClickSpace == false)
            {
                roadPrefab = platformBehaviour.InstantiateRoad();
                firstClickSpace = true;
            }

            if (roadPrefab != null)
            {
                Resize(roadPrefab.transform, 1, new Vector3(0f, 0.01f, 0f));
            }
        }
        else if(firstClickSpace)
        {
            Debug.Log("Rotacionando a rua. Rua: " + platformBehaviour.GetRoadPoint().position.ToString());
            player.SetState(new RotateRoadState(player, roadPrefab, platformBehaviour.GetRoadPoint()));
        }       
    }

    private void Resize(Transform currentPlatform, float amount, Vector3 direction)
    {
        currentPlatform.position += direction * amount / 2;

        currentPlatform.localScale += direction * amount;
    }
}
