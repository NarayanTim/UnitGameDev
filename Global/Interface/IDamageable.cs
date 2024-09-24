/*
    Name: Laxmi Timsina
    File Name: IDamageable.cs
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable{
    void TakeDamage(float damage,  bool isCritical);
}
