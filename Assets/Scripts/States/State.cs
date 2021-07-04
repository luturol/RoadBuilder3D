using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected PlayerBehaviour player;

    public State(PlayerBehaviour player)
    {
        this.player = player;
    }

    public abstract void Tick();
    public virtual void OnStateEnter() { }
    public virtual void OnStateExist() { }

}
