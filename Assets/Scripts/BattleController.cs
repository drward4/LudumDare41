using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleAction
{
    Pass,
    Swing,
    Bunt,
    Lead,
    Steal,
    UsePotion,
    CastSpell,
    ThrowFastBall,
    ThrowCurveBall,
    ThrowFireBall
};


public class BattleController : MonoBehaviour
{
    public List<Player> HomeTeamPlayers;
    public int HomeTeamAtBatIndex = 0;

    public Dugout Dugout;
    public Base HomePlate;
    public Base FirstBase;
    public Base SecondBase;
    public Base ThirdBase;
    public Base PitchersMound;

    public CanvasGroup ActionWindowPanel;
    public Text CurrentActionLabel;
    public OptionsPanel OptionsPanel;
    public Image CurrentBaseIndicator;

    private BaseBattleState CurrentState;


    public void StartBattle()
    {
        Debug.Log("Starting Battle");
        this.CurrentState = new InitBattleState(this);
        this.CurrentState.EnterState();
    }


    public void SwitchToState(BaseBattleState state)
    {
        this.CurrentState.ExitState();
        this.CurrentState = state;
        this.CurrentState.EnterState();
    }



    public void MovePlayerToDugout(Player player)
    {
        this.Dugout.PushPlayer(player);
        player.CharacterRenderer.flipX = false;
    }


    public void MovePlayerToBase(Player player, Base targetBase)
    {
        player.transform.SetParent(targetBase.transform, false);
        player.CharacterRenderer.flipX = targetBase.FlipSprite;
        targetBase.CurrentPlayer = player;
    }

    
    private void Update()
    {
        if (this.CurrentState != null)
        {
            this.CurrentState.UpdateState();
        }
    }
}
