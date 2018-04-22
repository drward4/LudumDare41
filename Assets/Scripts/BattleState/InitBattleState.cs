using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitBattleState : BaseBattleState
{
    public InitBattleState(BattleController controller)
        : base(controller)
    {

    }


    public override void EnterState()
    {
        base.EnterState();

        int index = this.Controller.HomeTeamAtBatIndex;

        for (int i = 0; i < this.Controller.HomeTeamPlayers.Count; i++)
        {
            if (i == 0) // testing, switch back to zero
            {
                if (i == 0) this.Controller.MovePlayerToBase(this.Controller.HomeTeamPlayers[index], this.Controller.HomePlate);

                // testing purposes
                if (i == 1) this.Controller.MovePlayerToBase(this.Controller.HomeTeamPlayers[index], this.Controller.FirstBase);
                if (i == 2) this.Controller.MovePlayerToBase(this.Controller.HomeTeamPlayers[index], this.Controller.SecondBase);
                if (i == 3) this.Controller.MovePlayerToBase(this.Controller.HomeTeamPlayers[index], this.Controller.ThirdBase);
            }
            else
            {
                this.Controller.MovePlayerToDugout(this.Controller.HomeTeamPlayers[index]);
            }

            if (++index >= this.Controller.HomeTeamPlayers.Count)
            {
                index = 0;
            }            
        }

        this.Controller.SwitchToState(new MakeChoicesState(this.Controller));
    }
}
