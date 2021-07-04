using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameState : State
{
    private EndGameBehaviour endGameBehaviour;
    private bool win;
    public EndGameState(PlayerBehaviour player, EndGameBehaviour endGameBehaviour, bool win) : base(player) 
    { 
        this.endGameBehaviour = endGameBehaviour;          
        this.win = win;
    }

    public override void Tick() { }

    public override void OnStateEnter()
    {                
        endGameBehaviour.SetWinOrLose(win);      
        endGameBehaviour.gameObject.SetActive(true);        
        
        player.Stars *= 2;
        endGameBehaviour.SetStarsCount(player.Stars);

        Debug.Log("(Platform Final) duplicado stars. Total = " + player.Stars.ToString());
    }

    public override void OnStateExist()
    {
        endGameBehaviour.gameObject.SetActive(false);        
    }
}
