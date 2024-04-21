using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeManager : MonoBehaviour
{
    public float HP;
    public float AttackDMG;

    private void Update()
    {
        Debug.Log(HP);
    }

    public void TakeDmg(float amount)
    {
        HP -= amount;
    }

    public void DealDmg(GameObject target)
    {
        var atm = target.GetComponent<AttributeManager>();
        if (atm != null)
        {
            atm.TakeDmg(AttackDMG);
        }
    }
}
