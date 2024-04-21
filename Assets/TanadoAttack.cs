using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TanadoAttack : Player
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            P_DealDmg(collision.gameObject);
        }
    }
}
