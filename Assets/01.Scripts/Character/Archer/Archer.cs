// # System
using System.Collections;
using System.Collections.Generic;

// # Unity
using UnityEngine;
using Charater;

public class Archer : Character
{
    [SerializeField] private GameObject shotSfx;
    [SerializeField] private GameObject shotPos;

    protected override void Attack()
    {
        base.Attack();
        if(curAttackSpeed <= 0)
        Instantiate(shotSfx, shotPos.transform.position, Quaternion.Euler(shotPos.transform.rotation.x, shotPos.transform.rotation.y, shotPos.transform.rotation.z));
    }
}
