using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementMode { Walking, Running, Crouching, Proning, Sprinting };

[RequireComponent(typeof(Rigidbody))]

public class CharacterMovement : MonoBehaviour
{
    
    
    private Vector3 velocity;
    public Transform characterMesh;
    public float Speed = 5f;
    private Rigidbody rigidBody;
    private float SmoothSpeed;
    private float rotationSpeed = 5f;


    public float walkspeed = 5f;
    public float runspeed = 10f;
    public float crouchspeed = 5f;
    public float pronespeed = 2f;

    private MovementMode movementMode;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    

    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.Translate(velocity.normalized*Speed*Time.deltaTime);
        

        if (velocity.magnitude>0)
        {
            rigidBody.velocity = new Vector3(velocity.normalized.x * SmoothSpeed, rigidBody.velocity.y, velocity.normalized.z * SmoothSpeed);
            SmoothSpeed = Mathf.Lerp(SmoothSpeed, Speed, Time.deltaTime);
            //characterMesh.rotation = Quaternion.LookRotation(velocity);
            characterMesh.rotation = Quaternion.Lerp(characterMesh.rotation, Quaternion.LookRotation(velocity), Time.deltaTime * rotationSpeed);
        }
        else
        {
            SmoothSpeed = Mathf.Lerp(SmoothSpeed, 0, Time.deltaTime);
        }
    }

    public Vector3 Velocity { get => rigidBody.velocity; set => velocity = value; }

    public void SetMovementMode (MovementMode mode)
    {
        movementMode = mode;
        
        switch (mode)
        {
            case MovementMode.Walking:
                {
                    Speed = walkspeed;
                    break;
                }
                
            case MovementMode.Running:
                {
                    Speed = runspeed;
                    break;
                }
            case MovementMode.Crouching:
                {
                    Speed = crouchspeed;
                    break;
                }
            case MovementMode.Proning:
                {
                    Speed = pronespeed;
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
}