/*
    Name: Laxmi Timsina
    File Name: CameraManager.cs
*/


using UnityEngine;

public class CameraManager : MonoBehaviour{
    private static CameraManager instance;
    [Header("Main Camera")]
    [SerializeField] private Camera mainCamera;

    public static CameraManager Instance{get{return instance;} private set{instance = value;}}

    public Camera MainCamera{
        get{return mainCamera;}
        private set{mainCamera = value;}
    }

    public Transform MainCameraTransform{
        get{return mainCamera.transform;}
    }

    private void Awake() {
        if(instance == null){
            instance = this;
        }else{
            Destroy(gameObject);
        }
        MouseControl.LockMouse();
        mainCamera = Camera.main;
    }



}
