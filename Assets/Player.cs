using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody rb;
    Animator anim;
    public GameObject Effect_1;

    private int hashAttackCount = Animator.StringToHash("AttackCount");

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        TryGetComponent(out anim); // anim = GetComponent<Animator>();
    }

    private void Start()
    {

    }

    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            anim.SetTrigger("Attack");
            Instantiate(Effect_1);
        }
    }
    
    public int AttackCount
    {
        get => anim.GetInteger(hashAttackCount);
        set => anim.SetInteger(hashAttackCount, value);
    }

    private void Move()
    {
        float hor = Input.GetAxis("Horizontal");
    }
}
