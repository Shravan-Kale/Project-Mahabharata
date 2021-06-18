using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Character))]

public class PlayerAnimatorController : MonoBehaviour
{
    public Animator playerAnimator;
    private Character character;


    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerAnimator == null)
        {
            Debug.Log("No Valid Player Animator");
        }

        playerAnimator.SetFloat("Velocity", character.getVelocity());
    }
}
