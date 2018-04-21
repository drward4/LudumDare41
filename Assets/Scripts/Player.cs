using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public SpriteRenderer CharacterRenderer;
    public Animator AnimatorController;

    public void StartWalking()
    {
        this.AnimatorController.SetBool("IsWalking", true);
    }


    public void StopWalking()
    {
        this.AnimatorController.SetBool("IsWalking", false);
    }


    private void Update()
    {

    }
}
