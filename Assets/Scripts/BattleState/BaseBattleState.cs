using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBattleState
{
    protected BattleController Controller;

    public BaseBattleState(BattleController controller)
    {
        this.Controller = controller;
    }


    public virtual void EnterState()
    {

    }


    public virtual void ExitState()
    {

    }


    public virtual void UpdateState()
    {

    }
}
