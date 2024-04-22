using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;


public class TonadoAttack : PlayerStat
{
    public int TornadoDealing(int Hp)
    {
        Hp -= AttackDmg;
        return Hp;
    }
}
