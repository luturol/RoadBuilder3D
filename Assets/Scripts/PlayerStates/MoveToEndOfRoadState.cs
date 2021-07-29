using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToEndOfRoadState : State
{
    private Animator animator;
    private Rigidbody playerRigidBody;
    private Transform endRoad;

    public MoveToEndOfRoadState(PlayerBehaviour player, Transform endRoad) : base(player)
    {
        this.endRoad = endRoad;
    }

    public override void Tick()
    {
        //move to the end of the road
        animator.SetBool("Walk", true);
        //transform.position = Vector3.MoveTowards(transform.position, roadPrefab.GetEndOfRoad().transform.position, Time.deltaTime * 5);
        var moveTo = Vector3.MoveTowards(player.transform.position, endRoad.position, Time.deltaTime * player.GetMovementSpeed());
        playerRigidBody.MovePosition(moveTo);

        if (playerRigidBody.position.Equals(endRoad.position))
        {           
            animator.SetBool("Walk", false);
            player.SetState(new InstantiatePlatformState(player));
        }

    }

    public override void OnStateEnter()
    {
        animator = player.GetAnimator();
        playerRigidBody = player.GetRigidBody();
    }
}
