/*
    Name: Laxmi Timsina
    File Name: InteractionManager.cs
*/


using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractionManager : Singletons<InteractionManager>{
    [SerializeField] private GameObject currentGameObject;
    [SerializeField] private Transform currentSelection;
    [SerializeField] private LayerMask pickUpMask;
    [SerializeField] private IIntractable hoverOver;

    [Range(1f, 10f)]
    [SerializeField] private float distance = 3.5f;

    [SerializeField] private CameraManager cameraManager;

    [SerializeField] private GameObject hoverHolder;
    [SerializeField] private TextMeshProUGUI hoverDescription;
    [SerializeField] private Image progressedImage;

    [SerializeField] private Vector3 lookPosition = new Vector3(0.5f, 0.5f, 0f);

    [SerializeField] private KeyCode pickupKey = KeyCode.E;

    [Header("Hold")]
    [SerializeField] private float maxTimeToHold = 1f;
    [SerializeField] private float holdTime;


    [SerializeField] private Chest chest;
    private Gun gun = null;


    protected override void Awake(){
        base.Awake();
        hoverDescription = hoverHolder.GetComponentInChildren<TextMeshProUGUI>();
        progressedImage = hoverHolder.GetComponentInChildren<Image>();
    }


    private void Start() {
        cameraManager = CameraManager.Instance;
    }


    private void Update() {
        LookAtObject();
    }


    private void LookAtObject(){
        Ray rayOrigin = cameraManager.MainCamera.ViewportPointToRay(lookPosition);
        if(Physics.Raycast(rayOrigin, out RaycastHit hit, distance, pickUpMask)){
            currentGameObject = hit.transform.gameObject;
            PickUpObject(currentGameObject);
        }else{
            ResetPickup();
        }






    }

    private void TurnOnHoverHolder(){
        
        hoverHolder.SetActive(true);
    }

    private void TurnOffHoverHolder(){
        hoverHolder.SetActive(false);
    }


    private void PickUpObject(GameObject currentGameObject){
        currentSelection = currentGameObject.transform;
        if(currentSelection != null){
            hoverOver = currentSelection.transform.GetComponent<IIntractable>();
            
            chest = currentSelection.transform.GetComponent<Chest>();
            
            gun = currentSelection.transform.GetComponent<Gun>();

            if(hoverOver != null && chest != null){
                HandleChestOpen();
            }



            if(hoverOver != null){
                HandleHoverOverInteraction();
            }else if(gun != null){
                HandleGunInteraction();
            }
            

        }

    }




    private void HandleHoverOverInteraction(){
        TurnOnHoverHolder();
        
        hoverDescription.SetText(hoverOver.PickupInformation(currentSelection));
        
        if (hoverOver.ObjectInteractionType == IIntractable.InteractType.HOLD) {
            HandleHoldInteraction(hoverOver);
        } else if (hoverOver.ObjectInteractionType == IIntractable.InteractType.CLICK) {
            HandleClickInteraction(hoverOver);
        }
    }


    private void HandleChestOpen(){
        TurnOnHoverHolder();
        
        hoverDescription.SetText(hoverOver.PickupInformation(currentSelection));
        if(chest.IsOpened){
            progressedImage.gameObject.SetActive(false);
            return;
        }
        
        if (hoverOver.ObjectInteractionType == IIntractable.InteractType.HOLD) {
            HandleHoldInteraction(hoverOver);
        } else if (hoverOver.ObjectInteractionType == IIntractable.InteractType.CLICK) {
            HandleClickInteraction(hoverOver);
        }
    }

    private void HandleGunInteraction(){
        IIntractable weaponManagerPickUp = WeaponManager.Instance.GetComponent<IIntractable>();
        TurnOnHoverHolder();
        
        hoverDescription.SetText(weaponManagerPickUp.PickupInformation(currentSelection));

        if (weaponManagerPickUp.ObjectInteractionType == IIntractable.InteractType.HOLD) {
            HandleHoldInteraction(weaponManagerPickUp);
        } else if (weaponManagerPickUp.ObjectInteractionType == IIntractable.InteractType.CLICK) {
            HandleClickInteraction(weaponManagerPickUp);
        }
    }

    private void HandleHoldInteraction(IIntractable intractable) {
        
        if (Input.GetKey(pickupKey)){
            IncreaseHoldTime();

            if(GetHoldTime() > maxTimeToHold) {
                intractable.Interact(currentSelection);
                RestHoldTime();
                progressedImage.fillAmount = 0f;
            }
        } else{
            RestHoldTime();
        }

        progressedImage.fillAmount = GetHoldTime();
    }

    private void HandleClickInteraction(IIntractable intractable) {
        if (Input.GetKey(pickupKey)) {
            intractable.Interact(currentSelection);
        }
    }



    private void RestHoldTime(){
        holdTime = 0;

    }

    private void ResetPickup(){
        currentSelection = null;
        currentGameObject = null;
        hoverDescription.text = "";
        chest = null;
        gun = null;
        TurnOffHoverHolder();
        RestHoldTime();
    }


    private void IncreaseHoldTime(){
        holdTime += Time.deltaTime;
    }

    private float GetHoldTime(){
        return holdTime;
    }
}

