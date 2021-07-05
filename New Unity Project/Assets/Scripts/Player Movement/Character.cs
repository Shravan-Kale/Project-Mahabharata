using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private float forwardInput;
    private float rightInput;
    private Vector3 velocity;
    public CameraController cameraController;
    public CharacterMovement characterMovement;
    public PlayerAnimatorController playerAnimator;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddMovementInput(float forward , float right)
    {
        forwardInput = forward;
        rightInput = right;

        Vector3 camForward = cameraController.transform.forward;
        Vector3 camright = cameraController.transform.right;

        Vector3 translation = forward * cameraController.transform.forward;
        translation += right * cameraController.transform.right;
        translation.y = 0;

        if (translation.magnitude>0)
        {
            velocity = translation;
        }
        else
        {
            velocity = Vector3.zero;
        }
        characterMovement.Velocity = translation;
    }

    public float getVelocity()
    {
        
        return characterMovement.Velocity.magnitude;

    }

    public void TogglrRun(bool enable)
    {
        if (enable)
        {
            characterMovement.SetMovementMode(MovementMode.Running);
            playerAnimator.SetMovementMode(MovementMode.Running);
        }
        else
        {
            characterMovement.SetMovementMode(MovementMode.Walking);
            playerAnimator.SetMovementMode(MovementMode.Walking);
        }
    }

    public void ToggleCrouch()
    {
        if (characterMovement.GetMovementMode() != MovementMode.Crouching)
        {
            characterMovement.SetMovementMode(MovementMode.Crouching);
            playerAnimator.SetMovementMode(MovementMode.Crouching);
        }
        else
        {
            characterMovement.SetMovementMode(MovementMode.Walking);
            playerAnimator.SetMovementMode(MovementMode.Walking);
        }
    }
}
