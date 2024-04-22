using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    [SerializeField] protected int maxHp;
    [SerializeField] protected int curHp;

    [SerializeField] protected int AttackDmg;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float turnSpeed;
}
