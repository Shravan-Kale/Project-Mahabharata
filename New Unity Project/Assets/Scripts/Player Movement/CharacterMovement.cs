using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum MovementMode
{
    Walking,
    Running,
    Crouching,
    Proning,
    Sprinting
};

[RequireComponent(typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float GroundCheckOffset;
    [SerializeField] private float GroundCheckRadius;
    [SerializeField] private float wallRunOffset = 1; // defines how far we can start wall running
    [SerializeField] private LayerMask walkableLayerMask;

    private Vector3 _velocity;

    public Vector3 velocity
    {
        get =>
            rigidBody.velocity;
        set =>
            _velocity = value;
    }

    public Transform characterMesh;
    public float Speed = 5f;
    private Rigidbody rigidBody;
    private float SmoothSpeed;
    private float rotationSpeed = 5f;

    private bool isCharacterGrounded;

    private Vector3[] directions; // defines in which directions we check wall

    private MovementMode movementMode;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        directions = new Vector3[]
        {
            transform.forward, 
            transform.right,
            transform.right + transform.forward,
            -transform.right,
            -transform.right + transform.forward,
        };
        
        //transform.Translate(velocity.normalized*Speed*Time.deltaTime);

        if (_velocity.magnitude > 0)
        {
            rigidBody.velocity = new Vector3(_velocity.normalized.x * SmoothSpeed,
                                             rigidBody.velocity.y,
                                             _velocity.normalized.z * SmoothSpeed);
            SmoothSpeed = Mathf.Lerp(SmoothSpeed, Speed, Time.deltaTime);
            //characterMesh.rotation = Quaternion.LookRotation(velocity);
            characterMesh.rotation = Quaternion.Lerp(characterMesh.rotation,
                                                     Quaternion.LookRotation(_velocity),
                                                     Time.deltaTime * rotationSpeed);
        }
        else
        {
            SmoothSpeed = Mathf.Lerp(SmoothSpeed, 0, Time.deltaTime);
        }

        CheckIsCharacterGrounded();

        CheckIsTouchingWall();

        void CheckIsCharacterGrounded()
        {
            var overlapPoint = transform.position + new Vector3(0, -1, 0) * GroundCheckOffset;
            var colliderInfo = Physics.OverlapSphere(overlapPoint, GroundCheckRadius);
            isCharacterGrounded = colliderInfo.Length != 0;
        }

        void CheckIsTouchingWall()
        {
            if (directions.Select(direction => Physics.Raycast(transform.position,
                                                                direction,
                                                               wallRunOffset,
                                                               walkableLayerMask))
                          .Any(colliderInfo => colliderInfo))
            {
                Debug.Log("Wall run");
                rigidBody.useGravity = false;
            }
            else
            {
                Debug.Log("Don`t touch" + wallRunOffset);
                rigidBody.useGravity = true;
            }
        }
    }


    public void SetMovementMode(MovementMode mode)
    {
        movementMode = mode;

        switch (mode)
        {
            case MovementMode.Walking:
            {
                Speed = 5;
                break;
            }

            case MovementMode.Running:
            {
                Speed = 10;
                break;
            }
            case MovementMode.Crouching:
            {
                Speed = 4;
                break;
            }
            case MovementMode.Proning:
            {
                Speed = 2;
                break;
            }
            case MovementMode.Sprinting:
            {
                Speed = 14;
                break;
            }
        }
    }

    public MovementMode GetMovementMode()
    {
        return movementMode;
    }

    private void OnDrawGizmos()
    {
        DrawGroundChecker();

        void DrawGroundChecker()
        {
            // draw ground check
            var drawPoint = transform.position + new Vector3(0, -1, 0) * GroundCheckOffset;
            Gizmos.DrawSphere(drawPoint, GroundCheckRadius);
            
            // draw wall checkers
            foreach (var direction in directions)
            {
                Gizmos.DrawRay(transform.position, direction);
            }
        }
    }
}