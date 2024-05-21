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

    protected override void Start()
    {
        boxCollider = GetComponent<BoxCollider>();  
        base.Start();
        float orignalspeed = nmAgent.speed;
        float originalacceleration = nmAgent.acceleration;
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
        isSkill = false;
        boxCollider.enabled = false;
        other.gameObject.GetComponent<Character>().TakeDamage(skillDamage);
        float orignalspeed = nmAgent.speed;
        float originalacceleration = nmAgent.acceleration;
    }
}
