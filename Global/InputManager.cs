/*
    Name: Laxmi Timsina
    File Name: InputManager.cs
*/



using UnityEngine;

public class InputManager : Singletons<InputManager>{
    private float mouseInputX, mouseInputY;
    private float horizontalInput, verticalInput;

    public float MouseInputX{get{return mouseInputX;}}
    public float MouseInputY{get{return mouseInputY;}}
    public float HorizontalInput{get{return horizontalInput;}}
    public float VerticalInput{get{return verticalInput;}}


    protected override void Awake(){
        base.Awake();
    }


    private void Update(){
        GetMouseInput();
        GetMovementInput();
    }

    private void GetMovementInput(){
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }


    private void GetMouseInput(){
        mouseInputX = Input.GetAxis("Mouse X");
        mouseInputY = Input.GetAxis("Mouse Y");
    }



}
