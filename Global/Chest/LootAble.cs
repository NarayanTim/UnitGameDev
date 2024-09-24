/*
    Name: Laxmi Timsina
    File Name: LootAble.cs
*/


using System;
using System.Collections.Generic;
using UnityEngine;

public class LootAble : MonoBehaviour{

    [Header("Pre fill data")]
    [SerializeField] private List<LootChances> lootPossibilities;

    [Space(15)]
    [Header("Final Loot")]
    [SerializeField] private List<LootReceived> finalLoot;
    [SerializeField] private bool wasLootCalculated;

    [Space(15)]
    [Header("Loot Value")]
    [Range(1, 10)]
    [SerializeField] private int maxItems = 1;
    
    
    
    
    
    [Range(1, 2)]
    [SerializeField] private int lootStartValue = 1;
    [Range(3, 6)]
    [SerializeField] private int lootEndValue = 6;

    private void Awake() {

    }



    public void Loot(){
        Debug.LogWarning($"Max items is {maxItems}");
        if(!wasLootCalculated){
            List<LootReceived> received = new List<LootReceived>();

                foreach(LootChances loot in lootPossibilities){

                    int lootAmount = Utils.GetRandomValue(loot.AmountMin, loot.AmountMax + 1);
                    
                    if(lootAmount > 0){
                        LootReceived lootGain = new LootReceived(loot.Prefab, lootAmount, loot.Type);
                        received.Add(lootGain);
                    }

                    if(received.Count >= maxItems){
                        break;
                    }
                
                }

            finalLoot = received;
            wasLootCalculated = true;

            if(finalLoot.Count == 0 && lootPossibilities.Count > 0){
                int randomIndex = Utils.GetRandomValue(0, lootPossibilities.Count - 1);
                LootChances loot = lootPossibilities[randomIndex];
                
                LootReceived lootGain = new LootReceived(loot.Prefab, loot.AmountMin, loot.Type);
                
                finalLoot.Add(lootGain);
            }

            foreach(LootReceived loot in finalLoot){
                if(loot.Type == LootType.WEAPON){
                    
                    GameObject pf = loot.Prefab; 
                    Instantiate(pf, transform);
                    pf.GetComponent<WeaponAnimations>().IsInChest = true;
                    pf.transform.rotation = Quaternion.Euler(0, 90, -90);
                }
            }         

            // Gain loot from chest
            // foreach(LootReceived loot in finalLoot){
                
            //     if(loot.Type == LootType.WEAPON){
            //         GunSettings.WeaponType gun = WeaponManager.Instance.ActiveWeaponSlot.GetComponentInChildren<Gun>().WeaponSetting.WeaponMode;
            //         AlertManager.Instance.TriggerPickup(loot.Prefab.name, SpriteManager.GetWeaponSprite(gun));
                
            //     }else if(loot.Type == LootType.AMMO){
            //         AlertManager.Instance.TriggerPickup(loot.Prefab.name, SpriteManager.GetAmmoSprite(AmmoSetting.AmmoType.NONE));
            //     }
            // }

        }

    }


    public void Loot(Transform location){
        Debug.LogWarning($"Max items is {maxItems}");
        if(!wasLootCalculated){
            List<LootReceived> received = new List<LootReceived>();

                foreach(LootChances loot in lootPossibilities){

                    int lootAmount = Utils.GetRandomValue(loot.AmountMin, loot.AmountMax + 1);
                    
                    if(lootAmount > 0){
                        LootReceived lootGain = new LootReceived(loot.Prefab, lootAmount, loot.Type);
                        received.Add(lootGain);
                    }

                    if(received.Count >= maxItems){
                        break;
                    }
                
                }

            finalLoot = received;
            wasLootCalculated = true;

            if(finalLoot.Count == 0 && lootPossibilities.Count > 0){
                int randomIndex = Utils.GetRandomValue(0, lootPossibilities.Count - 1);
                LootChances loot = lootPossibilities[randomIndex];
                
                LootReceived lootGain = new LootReceived(loot.Prefab, loot.AmountMin, loot.Type);
                
                finalLoot.Add(lootGain);
            }

            foreach(LootReceived loot in finalLoot){
                if(loot.Type == LootType.WEAPON){
                    GameObject pf = loot.Prefab; 
                    // WeaponAnimations aim = pf.GetComponent<WeaponAnimations>();
                    GameObject newWeapon = Instantiate(pf, location);
                    WeaponAnimations aim = newWeapon.GetComponent<WeaponAnimations>();
                    aim.IsInChest = true;
                    
                    newWeapon.transform.position = new Vector3(-0f, 0.1f, 0.22f);
                    newWeapon.transform.rotation = Quaternion.Euler(0, 90, -90);
                    
                    // aim.ParentTransform.position = new Vector3(-0.8f, 0.3f, 2.1f);
                    // aim.ParentTransform.rotation = Quaternion.Euler(0, 90, -90);

                }
            }         

            // Gain loot from chest
            // foreach(LootReceived loot in finalLoot){
                
            //     if(loot.Type == LootType.WEAPON){
            //         GunSettings.WeaponType gun = WeaponManager.Instance.ActiveWeaponSlot.GetComponentInChildren<Gun>().WeaponSetting.WeaponMode;
            //         AlertManager.Instance.TriggerPickup(loot.Prefab.name, SpriteManager.GetWeaponSprite(gun));
                
            //     }else if(loot.Type == LootType.AMMO){
            //         AlertManager.Instance.TriggerPickup(loot.Prefab.name, SpriteManager.GetAmmoSprite(AmmoSetting.AmmoType.NONE));
            //     }
            // }

        }

    }

}

[System.Serializable]

public class LootReceived{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int amount;
    [SerializeField] private LootType type;
    public GameObject Prefab{get { return prefab; } set { prefab = value;}}
    public int Amount{get { return amount; } set { amount = value;}}
    public LootType Type{get { return type; }}
    
    public LootReceived(){}
    
    public LootReceived(GameObject pf, int amount, LootType type){
        this.prefab = pf;
        this.amount = amount;
        this.type = type;
    }
}



[System.Serializable]
public class LootChances{
    [SerializeField] private GameObject prefab;
    [SerializeField] private LootType type;
    [SerializeField] private int amountMin;
    [SerializeField] private int amountMax;
    public GameObject Prefab{get { return prefab; } set { prefab = value;}}
    public int AmountMin{get { return amountMin; } set { amountMin = value;}}
    public int AmountMax{get { return amountMax; } set { amountMax = value;}}
    public LootType Type{get { return type; }}
}

public enum LootType{
    WEAPON,
    AMMO,
    OTHER
}