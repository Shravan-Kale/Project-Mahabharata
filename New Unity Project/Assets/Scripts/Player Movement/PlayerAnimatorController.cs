using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Character))]

public class PlayerAnimatorController : MonoBehaviour
{
    // TODO: remove serialize field
    [SerializeField] private Animator playerAnimator;
    private Character character;
    
    // static variables
    private static Animator _animator;
    private static readonly int PickUp = Animator.StringToHash("PickUp");
    private static readonly int Velocity = Animator.StringToHash("Velocity");


    // Start is called before the first frame update
    void Start()
    {
        _animator = playerAnimator;
        character = GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        _animator.SetFloat(Velocity, character.getVelocity());
    }

    public static void InvokePickUpAnimation()
    {
        _animator.SetTrigger(PickUp);
    }
}
