using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBattleState : BaseGameState
{
    public GameBattleState(GameController controller)
        : base(controller)
    {

    }

    public override void EnterState()
    {
        base.EnterState();
        this.Controller.BattleController.StartBattle();
    }
}
