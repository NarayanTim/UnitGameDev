/*
    Name: Laxmi Timsina
    File Name: IInteractAble.cs
*/



using System;
using UnityEngine;
public interface IIntractable{
    enum InteractType{
        CLICK,
        HOLD,
    }

    InteractType ObjectInteractionType { get; set; }

    void Interact(Transform target);
    string PickupInformation(Transform target);
}
