using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody rb;
    Animator anim;
    public GameObject[] Effects;
    public Transform[] effectPos;
    private Transform AutoEffcetPos;

    private Transform tr;
    public float MoveSpeed;
    public float turnSpeed;
    private bool isAttack;
    
    PlayerAutoTarget playerAuto;

    private int hashAttackCount = Animator.StringToHash("AttackCount");

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        tr = GetComponent<Transform>();
        TryGetComponent(out anim); // anim = GetComponent<Animator>();
        playerAuto = GetComponent<PlayerAutoTarget>();
    }

    private void Start()
    {

    }
    private void FixedUpdate()
    {
        Move();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Attack");

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
        float ver = Input.GetAxis("Vertical");
        float rot = Input.GetAxis("Mouse X");
        Vector3 moveDir = (Vector3.forward * ver) + (Vector3.right * hor);
        
        
        PlayerMoveAnim(ver);
        tr.Translate(moveDir.normalized * MoveSpeed * Time.deltaTime);
        tr.Rotate(Vector3.up * turnSpeed * Time.deltaTime * rot);
    }

    void PlayerMoveAnim(float v)
    {
        anim.SetBool("Walk_F", v > 0f);

        anim.SetBool("Walk_B", v < 0f);

        anim.SetBool("Idle", v == 0);

    }

    public void AttackEffect(int num)
    {
        GameObject effect = Effects[num];

        if (num == 0)
        {
            tornadoAttack_1(effect);
        }
        if (num == 1)
        {
            tornadoAttack_2(effect);
        }
    }

    /// <summary>
    /// 기본공격 1번
    /// </summary>
    /// <param name="effect"></param> 생성할 이펙트
    private void tornadoAttack_1(GameObject effect)
    {
        GameObject Effect;
        AutoEffcetPos = playerAuto.enemyAround(); // 주변 가장 가까운 적 오토 타겟

        if (AutoEffcetPos == null)
        {
            Debug.Log("정면 공격");
            Effect = Instantiate(effect, effectPos[0].position, Quaternion.identity);
            Destroy(Effect, 3f);
        }
        else if (AutoEffcetPos != null)
        {
            Debug.Log("주변적 공격");
            Effect = Instantiate(effect, new Vector3(AutoEffcetPos.position.x, 0, AutoEffcetPos.position.z), Quaternion.identity);
            Destroy(Effect, 3f);
        }
    }

    /// <summary>
    /// 기본공격 2번
    /// </summary>
    /// <param name="effect"></param> 생성할 이펙트
    private void tornadoAttack_2(GameObject effect)
    {
        GameObject Effcet = Instantiate(effect, effectPos[0].position, tr.rotation);
        Destroy(Effcet, 3f);
    }
}
