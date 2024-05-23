// # System
using System.Collections;
using System.Collections.Generic;

// # Unity
using UnityEngine;
using Charater;

public class MountedKnight : Character
{
    [SerializeField] private bool skillUse = false;
    [SerializeField] private bool isSkill = false;
    [SerializeField] private BoxCollider boxCollider;

    [Header("Skill")]
    public float dashSpeed;
    public float dashTime;
    public int skillDamage;
    private float orignalspeed;
    private float originalacceleration;

    protected override void Start()
    {
        boxCollider = GetComponent<BoxCollider>();  
        base.Start();
        orignalspeed = nmAgent.speed;
        originalacceleration = nmAgent.acceleration;
        DashAttack();

    }

    private void DashAttack()
    {
        isSkill = true;
        nmAgent.speed = dashSpeed;
        nmAgent.acceleration = dashSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isSkill)
        {
            if (other.gameObject.GetComponent<Character>().team != team)
            {
                boxCollider.enabled = false;
                nmAgent.speed = orignalspeed;
                nmAgent.acceleration = originalacceleration;
                other.gameObject.GetComponent<Character>().TakeDamage(skillDamage);
                isSkill = false;
            }
        }
    }
}
