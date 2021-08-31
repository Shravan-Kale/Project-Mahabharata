using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterMovement : MonoBehaviour
{
   [SerializeField] private float walkSpeed = 5f;
   [SerializeField] private float sprintSpeed = 10f;
   [SerializeField] private float crouchSpeed = 5f;
   [SerializeField] private float rotationSpeed = 5f;

   // public static variables
   public static Vector3 velocity;

   // public variables
   public Transform characterMesh;

   // private variables
   private MovementMode _movementMode;
   private float _currentSpeed = -1;
   private Vector3 _movementVector;
   private Rigidbody _rb;
   private Transform _cameraTransform;

   void Start()
   {
      _rb = GetComponent<Rigidbody>();
      _cameraTransform = Camera.main.transform;
   }

   void FixedUpdate()
   {
      if (CsGlobal.isPlayerMoving == false)
      {
         SetMovementMode();
         return;
      }

      if (CsGlobal.horizontalRawAxis != 0)
         velocity = _cameraTransform.right * CsGlobal.horizontalRawAxis;
      if (CsGlobal.verticalRawAxis != 0)
         velocity = _cameraTransform.forward * CsGlobal.verticalRawAxis;
      velocity.y = 0;

      Movement();
      Rotation();
      SetMovementMode();

      void Movement()
      {
         _movementVector = new Vector3(velocity.normalized.x,
                                       _rb.velocity.y,
                                       velocity.normalized.z);
         transform.Translate(_movementVector * (Time.fixedDeltaTime * _currentSpeed));
      }

      void Rotation()
      {
         // rotation
         if (velocity.magnitude > 0)
         {
            if (EnemySelector.isTargetSelected == false)
               characterMesh.rotation = Quaternion.Lerp(characterMesh.rotation,
                                                        Quaternion.LookRotation(velocity),
                                                        Time.deltaTime * rotationSpeed);
            else
            {
               var target = new Vector3(EnemySelector._closestEnemy.transform.position.x,
                                        transform.position.y,
                                        EnemySelector._closestEnemy.transform.position.z);
               transform.LookAt(target);
            }
         }
      }
   }


   public void SetMovementMode()
   {
      if (CsGlobal.isPressingShift)
         _movementMode = MovementMode.Sprinting;
      else if (CsGlobal.isPressingControl)
         _movementMode = MovementMode.Crouching;
      else if (CsGlobal.isPlayerMoving)
         _movementMode = MovementMode.Walking;
      else
         _movementMode = MovementMode.Idle;

      PlayerAnimatorController.SetMovementMode(_movementMode);

      switch (_movementMode)
      {
         case MovementMode.Walking:
         {
            _currentSpeed = walkSpeed;
            break;
         }

         case MovementMode.Sprinting:
         {
            _currentSpeed = sprintSpeed;
            break;
         }
         case MovementMode.Crouching:
         {
            _currentSpeed = crouchSpeed;
            break;
         }
      }
   }

   public MovementMode GetMovementMode()
   {
      return _movementMode;
   }
}