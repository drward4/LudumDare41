using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public BattleController BattleController;
    private BaseGameState CurrentState;

    private void Awake()
    {
        
    }


    private void Start()
    {
        this.CurrentState = new GameBattleState(this);
        this.CurrentState.EnterState();
    }


    private void Update()
    {
        this.CurrentState.UpdateState();
    }
}
