/*
    Name: Laxmi Timsina
    File Name: WeaponManager.cs
*/


using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : Singletons<WeaponManager>, IIntractable{
    [SerializeField] private List<GameObject> weaponSlots;
    [SerializeField] private GameObject activeWeaponSlot;
    
    public GameObject ActiveWeaponSlot { get{return activeWeaponSlot;} }
    public List<GameObject> WeaponSlots { get{return weaponSlots;} }

    public IIntractable.InteractType ObjectInteractionType { get; set;}
    
    [SerializeField] private GameObject emptySlot;
    [SerializeField] private bool weaponHasBeenAdded;

    [Header("Ammo Amount")]
    [SerializeField] private int totalLightAmmo = 0;
    [SerializeField] private int totalMediumAmmo = 0;
    [SerializeField] private int totalHeavyAmmo = 0;
    [SerializeField] private int totalRocketsAmmo = 0;
    [SerializeField] private int totalShellAmmo = 0;
    [SerializeField] private int totalEnergyAmmo = 0;

    public int TotalLightAmmo { get{return totalLightAmmo;} set{totalLightAmmo = value;}}
    public int TotalMediumAmmo { get{return totalMediumAmmo;} set{totalMediumAmmo = value;}}
    public int TotalHeavyAmmo { get{return totalHeavyAmmo;} set{totalHeavyAmmo = value;}}
    public int TotalShellAmmo { get{return totalShellAmmo;} set{totalShellAmmo = value;}}
    public int TotalRocketsAmmo { get{return totalRocketsAmmo;} set{totalRocketsAmmo = value;}}
    public int TotalEnergyAmmo { get{return totalEnergyAmmo;} set{totalEnergyAmmo = value;}}
    
    
    
    
    
    [Space(15)]
    [Header("Input Keys")]
    [SerializeField] private KeyCode changeSlotOne = KeyCode.Alpha1;
    [SerializeField] private KeyCode changeSlotTwo = KeyCode.Alpha2;
    [SerializeField] private KeyCode changeSlotThree = KeyCode.Alpha3;
    [SerializeField] private KeyCode dropWeapon = KeyCode.E;
    [SerializeField] private KeyCode throwGrenade = KeyCode.G;
    [SerializeField] private KeyCode throwSmokeGrenade = KeyCode.T;

    protected override void Awake() {
        base.Awake();
        ObjectInteractionType = IIntractable.InteractType.CLICK; 
        activeWeaponSlot = weaponSlots[0];
    }




    private void Update() {
        WeaponSlotCheck();
        GetChangeSlotInput();
    }


    private void GetChangeSlotInput(){

        if(Input.GetKeyDown(changeSlotOne)){
            SwitchWeaponSlots(0);
        }

        if(Input.GetKeyDown(changeSlotTwo)){
            SwitchWeaponSlots(1);
            
        }

        if(Input.GetKeyDown(changeSlotThree)){
            SwitchWeaponSlots(2);
        }

    }

    private void SwitchWeaponSlots(int index){
        if(index < weaponSlots.Count){
            activeWeaponSlot = weaponSlots[index];
        }
    }

    private void CheckEmptySlot(){
        emptySlot = weaponSlots.Find(slot => slot.transform.childCount == 0);
    }    


    private void WeaponSlotCheck(){
        foreach(GameObject slot in weaponSlots){
            if(slot == activeWeaponSlot){
                activeWeaponSlot.SetActive(true);
            }else{
                slot.SetActive(false);
            }
        }
    }

    private void AddWeaponToActiveAvailableSlot(Gun weapon){
        CheckEmptySlot();
        GameObject emptySlotToBeUsed = emptySlot != null ? emptySlot : activeWeaponSlot;
        if(emptySlotToBeUsed.transform.childCount > 0){
            DropCurrentWeapon(weapon);
        }

        weapon.transform.SetParent(emptySlotToBeUsed.transform, false);
        Gun currentWeapon = weapon;
        weapon.gameObject.transform.SetLocalPositionAndRotation(
            new Vector3(
                currentWeapon.WeaponSetting.NormalPosition.x,
                currentWeapon.WeaponSetting.NormalPosition.y,
                currentWeapon.WeaponSetting.NormalPosition.z
            ), Quaternion.Euler(
                currentWeapon.WeaponSetting.NormalRotation.x,
                currentWeapon.WeaponSetting.NormalRotation.y,
                currentWeapon.WeaponSetting.NormalRotation.z
            ));
        currentWeapon.IsActivateWeapon = true;
        weaponHasBeenAdded = true;
    }

    private void DropCurrentWeapon(Gun weapon){
        if(activeWeaponSlot.transform.childCount > 0){
            GameObject weaponToDrop = activeWeaponSlot.transform.GetChild(0).gameObject;
            Gun currentWeapon = weaponToDrop.GetComponent<Gun>();
            currentWeapon.IsActivateWeapon = false;
            currentWeapon.transform.SetParent(weapon.transform.parent);
            
            currentWeapon.transform.SetLocalPositionAndRotation(
                weapon.transform.localPosition, 
                weapon.transform.localRotation
            );
            
            weaponHasBeenAdded = false;
        }
    }



    public void DecreaseWeaponAmmo(int bulletsLeft, GunSettings.WeaponType weaponMode){
        switch(weaponMode){
            case GunSettings.WeaponType.ASSAULT_RIFLE:
                totalLightAmmo -= bulletsLeft;
                break;
            case GunSettings.WeaponType.PISTOL:
                totalMediumAmmo -= bulletsLeft;
                break;
            case GunSettings.WeaponType.SHOTGUN:
                totalShellAmmo -= bulletsLeft;
                break;
            case GunSettings.WeaponType.LUNCHER:
                totalRocketsAmmo -= bulletsLeft;
                break;
            default:
                Debug.LogWarning("Noting can happen");
                break;
        }
    }




    public int GetAmmoCount(GunSettings.WeaponType weapon){
        switch(weapon){
            case GunSettings.WeaponType.ASSAULT_RIFLE:
                return TotalLightAmmo;
            case GunSettings.WeaponType.PISTOL:
                return TotalMediumAmmo;
            case GunSettings.WeaponType.SHOTGUN:
                return totalShellAmmo;
            case GunSettings.WeaponType.LUNCHER:
                return totalRocketsAmmo;
            default:
                Debug.LogWarning("Not valid");
                return 0;
        }
    }









    // Interface

    public void Interact(Transform target){
        AddWeaponToActiveAvailableSlot(target.GetComponent<Gun>());
    }

    public string PickupInformation(Transform target){
        Gun gun = target.GetComponent<Gun>();
        return $"Pick up  {gun.WeaponSetting.GunName}";
    }

}
