using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Sprite Fighter_idle_sprite;
    public Sprite Thief_idle_sprite;
    public Sprite BlackMage_idle_sprite;
    public Sprite WhiteMage_idle_sprite;

    private BaseGameState CurrentState;

    private void Awake()
    {
        
    }


    private void Start()
    {
        this.CurrentState = new BattleState(this);
    }


    private void Update()
    {
        this.CurrentState.UpdateState();
    }
}
