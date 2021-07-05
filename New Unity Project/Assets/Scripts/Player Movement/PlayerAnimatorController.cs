using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Character))]

public class PlayerAnimatorController : MonoBehaviour
{
    public Animator playerAnimator;
    private Character character;
    private float speed;


    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<Character>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerAnimator == null)
        {
            Debug.Log("No Valid Player Animator");
        }

        speed = Mathf.SmoothStep(speed, character.getVelocity(), Time.deltaTime * 100);

        playerAnimator.SetFloat("Velocity", speed);
    }

    public void SetMovementMode(MovementMode mode)
    {


        switch (mode)
        {
            case MovementMode.Walking:
                {
                    playerAnimator.SetInteger("Movement-State", 0);
                    break;
                }

            case MovementMode.Running:
                {
                    playerAnimator.SetInteger("Movement-State", 0);
                    break;
                }
            case MovementMode.Crouching:
                {
                    playerAnimator.SetInteger("Movement-State", 1);
                    break;
                }
            case MovementMode.Proning:
                {
                    playerAnimator.SetInteger("Movement-State", 2);
                    break;
                }
            case MovementMode.Sprinting:
                {
                    playerAnimator.SetInteger("Movement-State", 3);
                    break;
                }
        }
    }
}