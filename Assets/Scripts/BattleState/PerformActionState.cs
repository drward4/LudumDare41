using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HitResultType
{
    Miss,
    GroundSingle,
    GroundDouble,
    GroundOut,
    GroundOutDoublePlay,
    FlyBallSingle,
    FlyBallDouble,
    FlyBallTriple,
    FlyBallHomeRun,
    FlyBallOut
}


public class PerformActionState : BaseBattleState
{
    protected Queue<Base> RemainingBases;
    protected Base CurrentBase;

    private float WaitTimeRemaining;

    public PerformActionState(BattleController controller, Queue<Base> remainingBases)
        : base(controller)
    {
        this.RemainingBases = remainingBases;
    }


    private bool Roll(float successChance)
    {
        float result = Random.Range(0f, 1f);
        Debug.Log("Roll < " + successChance.ToString("F2") + " to succeed: " + result.ToString("F2") + " == " + (result < successChance).ToString());
        return result < successChance;
    }

    public HitResultType DeterminePitchOutcome(Player pitcher, Player hitter)
    {
        HitResultType result = HitResultType.Miss;

        bool didHit = this.Roll(hitter.CurrentAction == BattleAction.Bunt ? 0.8f : 0.5f);
        Debug.Log("Hit: " + didHit);

        if (didHit)
        {
            if (hitter.CurrentAction == BattleAction.Bunt)
            {
                bool didReachBase = this.Roll(0.8f);

                if (didReachBase)
                {
                    if (this.Roll(0.2f))
                    {
                        result = HitResultType.GroundSingle;
                    }
                    else
                    {
                        result = HitResultType.GroundOut;
                    }
                }
            }
            else
            {
                bool didReachBase = this.Roll(0.4f);

                if (didReachBase)
                {
                    int outcome = Random.Range(0, 6);
                    switch (outcome)
                    {
                        case 0: result = HitResultType.GroundSingle; break;
                        case 1: result = HitResultType.GroundDouble; break;
                        case 2: result = HitResultType.FlyBallSingle; break;
                        case 3: result = HitResultType.FlyBallDouble; break;
                        case 4: result = HitResultType.FlyBallTriple; break;
                        case 5: result = HitResultType.FlyBallHomeRun; break;
                    }
                }
                else
                {
                    int outcome = Random.Range(0, 2);
                    switch (outcome)
                    {
                        case 0: result = HitResultType.GroundOut; break;
                        case 1: result = HitResultType.FlyBallOut; break;
                    }

                }
            }
        }
        else
        {
            result = HitResultType.Miss;
        }

        return result;
    }


    public override void EnterState()
    {
        base.EnterState();

        if (this.RemainingBases.Count <= 0)
        {
            if (this.Controller.HomePlate.CurrentPlayer.CurrentAction == BattleAction.Swing ||
                this.Controller.HomePlate.CurrentPlayer.CurrentAction == BattleAction.Bunt)
            {

            }


            this.Controller.SwitchToState(new MakeChoicesState(this.Controller));
        }
        else
        {
            this.PerformAction();
        }
    }

    protected bool IsOut(HitResultType result)
    {
        bool isOut = false;

        switch (result)
        {
            case HitResultType.FlyBallOut:
            case HitResultType.GroundOut:
            case HitResultType.GroundOutDoublePlay:
                isOut = true;
                break;
        }

        return isOut;
    }


    protected string GetResultString(HitResultType result, BattleAction action)
    {
        string ret = action == BattleAction.Bunt ? "BUNTS AND " : "SWINGS AND ";

        switch (result)
        {
            case HitResultType.Miss: ret += "MISSES!"; break;
            case HitResultType.GroundSingle: ret += "HITS A SINGLE!"; break;
            case HitResultType.GroundDouble: ret += "HITS A DOUBLE!!"; break;
            case HitResultType.GroundOut: ret += "GROUNDS OUT."; break;
            case HitResultType.GroundOutDoublePlay: ret += "GROUNDS INTO A DOUBLE PLAY."; break;
            case HitResultType.FlyBallSingle: ret += "HITS A FLY BALL SINGLE!"; break;
            case HitResultType.FlyBallDouble: ret += "HITS A FLY BALL DOUBLE!!"; break;
            case HitResultType.FlyBallTriple: ret += "HITS A FLY BALL TRIPLE!!!"; break;
            case HitResultType.FlyBallHomeRun: ret += "HITS A HOME RUN!!!!"; break;
            case HitResultType.FlyBallOut: ret += "FLIES OUT."; break;
        }

        return ret;
    }


    protected void PerformAction()
    {
        this.CurrentBase = this.RemainingBases.Dequeue();

        Debug.Log("Peformaing action, " + this.RemainingBases.Count + " remaining");

        string actionString = this.CurrentBase.CurrentPlayer.name.ToUpper() + " ";
        HitResultType result = HitResultType.Miss;

        switch (this.CurrentBase.CurrentPlayer.CurrentAction)
        {
            case BattleAction.Pass: actionString += " PASSES..."; break;
            case BattleAction.Lead: actionString += " TAKES A LEAD..."; break;
            case BattleAction.Steal:
                this.CurrentBase.CurrentPlayer.StartWalking();
                actionString += " STEALS!"; break;
            case BattleAction.UsePotion: actionString += " USES A POTION"; break;
            case BattleAction.CastSpell: actionString += " CASTS A SPELL"; break;
            case BattleAction.Swing:
            case BattleAction.Bunt:
                result = this.DeterminePitchOutcome(this.Controller.PitchersMound.CurrentPlayer, this.Controller.HomePlate.CurrentPlayer);
                actionString += " " + this.GetResultString(result, this.Controller.HomePlate.CurrentPlayer.CurrentAction); break;
            case BattleAction.ThrowFastBall:
            case BattleAction.ThrowCurveBall:
            case BattleAction.ThrowFireBall:
                actionString += " THROWS..";
                break;
        }

        this.Controller.CurrentActionLabel.text = actionString;

        if (this.IsOut(result))
        {
            Player nextPlayer = this.Controller.Dugout.PopPlayer();
            this.Controller.MovePlayerToDugout(this.Controller.HomePlate.CurrentPlayer);
            this.Controller.MovePlayerToBase(nextPlayer, this.Controller.HomePlate);

            this.Controller.SwitchToState(new MakeChoicesState(this.Controller));
        }
        else
        {
            this.WaitTimeRemaining = 2f;
        }
    }


    public override void UpdateState()
    {
        base.UpdateState();

        if (this.WaitTimeRemaining > 0f)
        {
            this.WaitTimeRemaining -= Time.deltaTime;

            if (this.WaitTimeRemaining <= 0f)
            {
                this.Controller.SwitchToState(new PerformActionState(this.Controller, this.RemainingBases));
            }
        }
    }
}

