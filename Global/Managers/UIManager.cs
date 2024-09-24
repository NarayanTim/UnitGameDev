/*
    Name: Laxmi Timsina
    File Name: UIManager.cs
*/


using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singletons<UIManager>{

    [Header("Icons")]
    [SerializeField] private GameObject centerDot;
    [SerializeField] private GameObject centerHand;
    [SerializeField] private GameObject centerAim;

    [Header("Weapon Panel")]
    [Space(5)]
    [Header("Ammo")]
    [SerializeField] private TextMeshProUGUI magazineAmount;
    [SerializeField] private TextMeshProUGUI totalAmmo;
    [SerializeField] private Image ammoType;
    [Space(10)]
    [Header("Weapons")]
    [SerializeField] private Image activateWeaponUI;
    [SerializeField] private Image unActivateWeaponOneUI;
    [SerializeField] private Image unActivateWeaponTwoUI;


    [Header("Extra")]
    [SerializeField] private Sprite emptySlot;

    [Header("Others")]
    [SerializeField] private float time = 0.55f;


    public GameObject CenterDot { get{return centerDot;}}
    public GameObject CenterHand { get{return centerHand;}}
    public GameObject CenterAim { get{return centerAim;}}

    protected override void Awake(){
        base.Awake();
    }

    private void Update() {
        // SetUpUIWithWeapon();
    }



    // private void SetUpUIWithWeapon(){
    //     Gun activeWeapon = WeaponManager.Instance.ActiveWeaponSlot.GetComponentInChildren<Gun>();
        
    //     GameObject firstSlot = GetUnActiveWeaponSlot();
    //     Gun UnActiveWeaponOne = firstSlot?.GetComponentInChildren<Gun>();

    //     GameObject secondSlot = GetUnActiveWeaponSlot(firstSlot);
    //     Gun UnActiveWeaponTwo = secondSlot?.GetComponentInChildren<Gun>();

    //     if(activeWeapon){
    //         WeaponSettings.WeaponType model = activeWeapon.WeaponSetting.WeaponMode;
            
            
    //         magazineAmount.SetText($"{activeWeapon.BulletLeft / activeWeapon.WeaponSetting.BulletsPerShot}");
    //         // totalAmmo.SetText($"{activeWeapon.MagazineSize / activeWeapon.BulletsPerShot}");
    //         totalAmmo.SetText($"{WeaponManager.Instance.CheckAmmoLeft(model)}");
            
            
            
    //         ammoType.sprite = GetAmmoSprite(model);
    //         activateWeaponUI.sprite = GetWeaponSprite(model);
            
    //         if(UnActiveWeaponOne){
    //             unActivateWeaponOneUI.sprite = GetWeaponSprite(UnActiveWeaponOne.WeaponSetting.WeaponMode);
    //         }

    //         if(UnActiveWeaponTwo){
    //             unActivateWeaponTwoUI.sprite = GetWeaponSprite(UnActiveWeaponTwo.WeaponSetting.WeaponMode);
    //         }

            
    //     }else{
    //         magazineAmount.SetText("");
    //         totalAmmo.SetText("");

    //         ammoType.sprite = emptySlot;
    //         activateWeaponUI.sprite = emptySlot;
    //         unActivateWeaponOneUI.sprite = emptySlot;
    //         unActivateWeaponTwoUI.sprite = emptySlot;


    //     }
    
    
    
    
    
    // }

    // private GameObject GetUnActiveWeaponSlot(){
    //     foreach (GameObject slot in WeaponManager.Instance.WeaponSlots){
    //         if(slot != WeaponManager.Instance.ActiveWeaponSlot){
    //             return slot;
    //         }
    //     }
    //     return null;
    // }

    // private GameObject GetUnActiveWeaponSlot(GameObject otherSlot){
    //     foreach (GameObject slot in WeaponManager.Instance.WeaponSlots){
    //         if(slot != WeaponManager.Instance.ActiveWeaponSlot && slot != otherSlot){
    //             return slot;
    //         }
    //     }
    //     return null;
    // }    

    // private Sprite GetWeaponSprite(WeaponSettings.WeaponType model){
    //     switch(model){
    //         case WeaponSettings.WeaponType.AUTOMATIC:
    //             return GameAssets.Assets.automaticImage.GetComponent<SpriteRenderer>().sprite;
            
    //         case WeaponSettings.WeaponType.BURST:
    //             return GameAssets.Assets.burstImage.GetComponent<SpriteRenderer>().sprite;
            
    //         case WeaponSettings.WeaponType.PISTOL:
    //             return GameAssets.Assets.pistolImage.GetComponent<SpriteRenderer>().sprite;
            
    //         case WeaponSettings.WeaponType.SHOTGUN:
    //             return GameAssets.Assets.shotgunImage.GetComponent<SpriteRenderer>().sprite;

    //         case WeaponSettings.WeaponType.LUNCHER:
    //             return GameAssets.Assets.luncherImage.GetComponent<SpriteRenderer>().sprite;
    //         default:
    //             Debug.LogWarning($"{model} is not valid");
    //             return null;
    //     }
    // }

    // private Sprite GetAmmoSprite(WeaponSettings.WeaponType model){
    //     switch(model){
    //         case WeaponSettings.WeaponType.AUTOMATIC:
    //         case WeaponSettings.WeaponType.BURST:
    //             return GameAssets.Assets.rifleAmmoImage.GetComponent<SpriteRenderer>().sprite;
    //         case WeaponSettings.WeaponType.PISTOL:
    //             return GameAssets.Assets.pistolAmmoImage.GetComponent<SpriteRenderer>().sprite;
    //         case WeaponSettings.WeaponType.SHOTGUN:
    //             return GameAssets.Assets.shotgunAmmoImage.GetComponent<SpriteRenderer>().sprite;
    //         case WeaponSettings.WeaponType.LUNCHER:
    //             return GameAssets.Assets.luncherAmmoImage.GetComponent<SpriteRenderer>().sprite;
    //         default:
    //             Debug.LogWarning($"is not valid - Ammo {model}");
    //             return null;
    //     }
    // }

    
    
    
    
    public void ShowCenterDot(){
        centerHand.SetActive(false);
        centerAim.SetActive(false);
        centerDot.SetActive(true);
    }

    public void ShowCenterHand(){
        centerDot.SetActive(false);
        centerAim.SetActive(false);
        centerHand.SetActive(true);
    }

    public void ShowCenterAim(){
        centerDot.SetActive(false);
        centerHand.SetActive(false);
        centerAim.SetActive(true);
    }


}
