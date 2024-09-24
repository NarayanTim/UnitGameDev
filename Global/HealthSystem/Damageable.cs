/*
    Name: Laxmi Timsina
    File Name: Damageable.cs
*/


using UnityEngine;
using System;

public class Damageable : MonoBehaviour{
    public event Action<float, float> OnHealthChanged; // Event for health changes
    public event Action<float, float> OnShieldChanged; // Event for shield changes
    public event Action<Vector3, float, bool> OnDamagePopup;

    [Header("Health")]
    [SerializeField] private float currentHealth = 100f;
    [SerializeField] private float maxHealth = 100f;

    [Header("Shield")]
    [SerializeField] private float currentShield = 100f;
    [SerializeField] private float maxShield = 100f;

    [Header("State")]
    [SerializeField] private bool isAlive = true;
    [SerializeField] private bool hasHeathBar;
    [SerializeField] private bool hasDamagePopUp;
    [SerializeField] private bool useShield;
    [SerializeField] private bool hasShield;

    // Properties
    // Health
    public float CurrentHealth { get{return currentHealth;} set{currentHealth = value;}}
    public float MaxHealth { get{return maxHealth;} set{maxHealth = value;}}

    // Shield
    public float CurrentShield { get{return currentShield;} set{currentShield = value;}}
    public float MaxShield { get{return maxShield;} set{maxShield = value;}}

    // state
    public bool IsAlive{get{return isAlive;} set{isAlive = value;}}
    public bool UseShield{get{return useShield;} private set{useShield = value;}}
    public bool IsPlayer{get{return hasHeathBar;} private set{hasHeathBar = value;}}
    public bool HasShield{get{return hasShield;} set{hasShield = value;}}

    private void Awake() {
        if(useShield || hasShield || currentShield > 0){
            useShield = true;
            hasShield = true;
        }

        if(hasHeathBar){
            // Trigger the initial events
            OnHealthChanged?.Invoke(currentHealth, maxHealth);
            OnShieldChanged?.Invoke(currentShield, maxShield);
        }


    }


    public void TakeDamage(float damage){
        if(!isAlive){
            Debug.LogWarning("Is not alive");
            return;
        }

        if(hasShield){
            float leftoverDamage = LossShield(damage);
            if(leftoverDamage >= 0){
                currentShield = 0;
                hasShield = false;
                currentHealth -= leftoverDamage;
            }
        }else{
            currentHealth -= damage;
        }
        
        // Clamp the health and shield values to ensure they are within their valid ranges
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        currentShield = Mathf.Clamp(currentShield, 0, maxShield);

        // Check if the entity is still alive
        if (currentHealth <= 0){
            isAlive = false;
        }

        // Trigger events
        if(hasHeathBar && !hasDamagePopUp){
            OnHealthChanged?.Invoke(currentHealth, maxHealth);
            OnShieldChanged?.Invoke(currentShield, maxShield);
        }




    }

    public void GainHealth(float healAmount){
        if(!isAlive){
            Debug.LogWarning("Not able to gain health");
            return;
        }

        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if(hasHeathBar){
            OnHealthChanged?.Invoke(currentHealth, maxHealth);
        }
    }

    public void GainShield(float gainAmount){
        if(!isAlive && !useShield){
            Debug.LogWarning("Not able to gain shield not using shield");
            return;
        }

        currentShield += gainAmount;
        currentShield = Mathf.Clamp(currentShield, 0, maxShield);
        hasShield = currentShield > 0;

        if(hasHeathBar){
            OnShieldChanged?.Invoke(currentShield, maxShield);
        }
    }



    private float LossShield(float amount){
        currentShield -= amount;
        if(currentShield <= 0){
            return Mathf.Abs(currentShield);
        }
        return -1;
    }



}
