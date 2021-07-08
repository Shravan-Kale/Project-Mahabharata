using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitchClient : MonoBehaviour
{
    CharacterMovement characterMovement;
    PlayerInput playerInput;
    PlayerAnimatorController playerAnimatorController;
    Character character;
    GameObject cam;
    CameraController cameraController;
    public bool starting = false;
    void Start()
    {
        characterMovement = GetComponentInParent<CharacterMovement>();
        playerInput = GetComponentInParent<PlayerInput>();
        playerAnimatorController = GetComponentInParent<PlayerAnimatorController>();
        character = GetComponentInParent<Character>();
        cam = transform.Find("CameraBase").gameObject;
        cameraController = GetComponentInParent<CameraController>();
        if(!starting)
        {
            Deactivate();
        }

    }
    public void Deactivate()
    {
        characterMovement.enabled = false;
        playerInput.enabled = false;
        playerAnimatorController.enabled = false;
        character.enabled = false;
        cam.SetActive(false);

    }
    public void Activate()
    {
        characterMovement.enabled = true;
        playerInput.enabled = true;
        playerAnimatorController.enabled = true;
        character.enabled = true;
        cam.SetActive(true);
    }
}
