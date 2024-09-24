/*
    Name: Laxmi Timsina
    File Name: GameAssets.cs
*/


using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour{
    [Header("Tracer")]
    public Transform bulletTracerPf;
    public List<Transform> bulletTracePfList;

    [Space(8)]
    [Header("Text ")]
    public Transform pfDamagePopup;

    [Space(8)]
    [Header("Bullet")]
    public Transform bulletPf;

    [Space(8)]
    [Header("Weapon")]
    public Transform automaticImage;
    public Transform burstImage;
    public Transform shotgunImage;
    public Transform pistolImage;
    public Transform luncherImage;

    [Space(10)]
    [Header("Ammo")]
    public Transform rifleAmmoImage;
    public Transform shotgunAmmoImage;
    public Transform pistolAmmoImage;
    public Transform luncherAmmoImage;

    [Header("Throwables")]
    [Space(15)]
    [Header("Effects")]
    public GameObject grenadeExplosionEffectPf;
    public GameObject smokeGrenadeExplosionEffectPf;
    [Space(8)]
    [Header("Throwable pf")]
    public GameObject grenadePf;
    public GameObject smokeGrenadePf;

    private static GameAssets _assets;

    public static GameAssets Assets{
        get{
            if(_assets == null){
                _assets = Instantiate(Resources.Load<GameAssets>("GameAssets"));
            }
            return _assets;
        }
    }


    public Transform GetBulletTracer(){
        if(bulletTracePfList.Count > 1){
            return bulletTracePfList[Utils.GetRandomValue(0, bulletTracePfList.Count)];
        }
        return bulletTracerPf;
    }


}
