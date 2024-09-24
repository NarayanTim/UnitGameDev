/*
    Name: Laxmi Timsina
    File Name: Chest.cs
*/


using UnityEngine;

public class Chest : MonoBehaviour, IIntractable{
    [SerializeField] private Animator animatorControl;
    [SerializeField] private bool isOpened = false;
    [SerializeField] private Transform weaponPlace;
    public IIntractable.InteractType ObjectInteractionType { get; set; }

    public Animator AnimatorControl { get{return animatorControl;}}
    public bool IsOpened { get{return isOpened;} set{isOpened = value;}}


    private void Awake() {
        animatorControl = GetComponent<Animator>();
        ObjectInteractionType = IIntractable.InteractType.HOLD;
        weaponPlace = transform.GetChild(0);
    }





    public void Interact(Transform target){
        if(!isOpened){
            LootAble lootAble = target.GetComponent<LootAble>();
            lootAble.Loot(weaponPlace);
            isOpened = true;
            animatorControl.SetBool("Open", true);

            // Done
            
        }
        
    }

    public string PickupInformation(Transform target){
        return IsOpened ? "Empty" : "Open Chest";
    }

}
