using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public int maxHP;
    public int curHP;

    Rigidbody rb;
    SphereCollider sphereCollider;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("TornadoAttack"))
        {
            TonadoAttack tornado = GetComponent<TonadoAttack>();
            curHP = tornado.TornadoDealing(curHP);
        }
    }
}
