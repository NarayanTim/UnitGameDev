/*
    Name: Laxmi Timsina
    File Name: WeaponCameraManager.cs
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCameraManager : MonoBehaviour{
    private static WeaponCameraManager instance;
    [Header("Weapon Camera")]
    [SerializeField] private Camera weaponCamera;

    public static WeaponCameraManager Instance{get{return instance;} private set{instance = value;}}

    public Camera WeaponCamera{
        get{return weaponCamera;}
        private set{weaponCamera = value;}
    }

    public Transform WeaponCameraTransform{
        get{return weaponCamera.transform;}
    }

    private void Awake() {
        if(instance == null){
            instance = this;
        }else{
            Destroy(gameObject);
        }
        MouseControl.LockMouse();
        weaponCamera = GetComponent<Camera>();
    }





}
