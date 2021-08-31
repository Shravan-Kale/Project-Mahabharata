using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    // public static variables
    public static Animator playerAnimator;
    private static readonly int State = Animator.StringToHash("State");

    void Awake()
    {
        playerAnimator = gameObject.GetComponentInChildren<Animator>();
    }
    
    public static void SetMovementMode(MovementMode mode)
    {
        switch (mode)
        {
            case MovementMode.Walking:
            {
                playerAnimator.SetInteger(State, 1);
                break;
            }
            case MovementMode.Crouching:
            {
                playerAnimator.SetInteger("Movement-State", 1);
                break;
            }
            case MovementMode.Sprinting:
            {
                if (playerAnimator)
                    playerAnimator.SetInteger(State,2);
                break;
            }
            default:
                playerAnimator.SetInteger(State,0);
                break;
        }
    }

    public static void TriggerAttackAnimation()
    {
        playerAnimator.SetTrigger("Attack");
    }
}