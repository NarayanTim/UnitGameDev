/*
    Name: Laxmi Timsina
    File Name: MouseControl.cs
*/


using UnityEngine;
public static class MouseControl {

    public static void UnlockMouse(){
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public static void LockMouse(){
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

}
