using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseGameState
{
    private GameController Controller;

    public BaseGameState(GameController controller)
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
