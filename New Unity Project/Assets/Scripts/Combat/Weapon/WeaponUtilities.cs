using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class WeaponUtilities : MonoBehaviour
{
   [SerializeField] public WeaponTypes weaponType;
   [Space] [SerializeField] public GameObject weaponContainer;
   [SerializeField] public WeaponComboChain[] weaponComboChains;
   [SerializeField] public float comboResetTimer;

   // public variables
   [HideInInspector] public int currentComboIndex = 0;
   [HideInInspector] public int comboChain = -1;

   //  protected variables
   protected Collider _collider;
   protected Rigidbody _rb;
   protected float previousTime;
   protected int previousComboChain;

   public enum WeaponTypes
   {
      melee,
      bow
   }

   public void InvokeAttackAnimation()
   {
      CheckTimer();
      GenerateComboChain();
      
      if (currentComboIndex == weaponComboChains[comboChain].weaponComboChain.Length)
      {
         currentComboIndex = 0;
         GenerateComboChain(true);
      }

      previousTime = Time.time;

      Debug.Log($"currentComboIndex: {currentComboIndex}, weaponComboChain: {comboChain}");

      PlayerAnimatorController.playerAnimator.Play(weaponComboChains[comboChain]
                                                   .weaponComboChain[currentComboIndex]
                                                   .animationsStateName);

      void CheckTimer()
      {
         if (Time.time - previousTime < comboResetTimer)
         {
            currentComboIndex++;
         }
         else
         {
            GenerateComboChain(true);
            currentComboIndex = 0;
         }
      }

      void GenerateComboChain(bool generateAnyway = false)
      {
         if (comboChain == -1 || generateAnyway)
         {
            Debug.Log("Start new combo chain");
            
            previousComboChain = comboChain;
            comboChain = Random.Range(0, weaponComboChains.Length);
            if (comboChain == previousComboChain)
            {
               if (comboChain + 1 < weaponComboChains.Length)
                  comboChain++;
               else
                  comboChain--;
            }
         }
      }
   }

#region weapon control

   protected void SetUpUtils()
   {
      previousTime = Time.time;
      _collider = transform.GetComponent<Collider>();
      _rb = transform.GetComponent<Rigidbody>();
   }

   public void PickUp(Transform weaponContainerTransform)
   {
      ChangeComponentActive(false);

      transform.position = weaponContainerTransform.position;
      transform.rotation = weaponContainerTransform.rotation;
      transform.SetParent(weaponContainerTransform);
   }

   public void Drop(Vector3 dropForce)
   {
      transform.parent = null;

      ChangeComponentActive(true);
      _rb.AddForce(dropForce);
   }

   private void ChangeComponentActive(bool isActive)
   {
      _rb.isKinematic = isActive == false;
      _rb.useGravity = isActive;
      _collider.enabled = isActive;
   }

#endregion

   [Serializable]
   public class WeaponComboChain
   {
      public WeaponComboElement[] weaponComboChain;
   }

   [Serializable]
   public class WeaponComboElement
   {
      public string animationsStateName;
      public float attackSpeed;
      public int damage;
   }
}