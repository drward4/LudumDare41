using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public SpriteRenderer CharacterRenderer;
    public Animator AnimatorController;

    public bool IsWalking = false;

    private float WalkTimeRemaining;
    private float TestWalkInterval = 2f;
    private float IdleTimeRemaining;

    private void Start()
    {
        this.IdleTimeRemaining = 2f;
    }

    private void Update()
    {
        if (this.IdleTimeRemaining > 0f)
        {
            this.IdleTimeRemaining -= Time.deltaTime;

            if (this.IdleTimeRemaining <= 0f)
            {
                this.WalkTimeRemaining += this.TestWalkInterval;
                this.AnimatorController.SetBool("IsWalking", true);

                this.IsWalking = true;
            }
        }
        else if (this.WalkTimeRemaining > 0f)
        {
            this.WalkTimeRemaining -= Time.deltaTime;

            if (this.WalkTimeRemaining <= 0f)
            {
                this.IdleTimeRemaining += 2f;
                this.AnimatorController.SetBool("IsWalking", false);
            }
        }

    }
}
