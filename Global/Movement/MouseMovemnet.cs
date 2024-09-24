/*
    Name: Laxmi Timsina
    File Name: MouseMovement.cs
*/

using UnityEngine;
public class MouseMovement : MonoBehaviour{
    [SerializeField] private InputManager inputManager;

    [Header("Mouse Sensitivity")]
    [SerializeField] private float sensitivityX;
    [SerializeField] private float sensitivityY;

    [Header("Mouse Look Angle")]
    [Range(-90, 90)]
    [SerializeField] private float minLookAngle = -90f;
    [Range(-90, 90)]
    [SerializeField] private float maxLookAngle = 90f;

    private float xRotation;
    private float yRotation;


    private void Awake(){
        MouseControl.LockMouse();
    }
    
    
    private void Start() {
        inputManager = InputManager.Instance;
    }

    private void Update(){
        GetMouseInput();

    }


    private void GetMouseInput(){

        float mouseX = inputManager.MouseInputX * sensitivityX * Time.deltaTime;
        float mouseY = inputManager.MouseInputY * sensitivityY * Time.deltaTime;


        // look up / down 
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minLookAngle, maxLookAngle);

        yRotation += mouseX;
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);

    }


}
