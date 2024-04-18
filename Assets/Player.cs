using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody rb;
    Animator anim;
    public GameObject[] Effects;
    public Transform[] effectPos;

    private Transform tr;
    public float MoveSpeed;
    public float turnSpeed;

    private int hashAttackCount = Animator.StringToHash("AttackCount");

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        tr = GetComponent<Transform>();
        TryGetComponent(out anim); // anim = GetComponent<Animator>();
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
        tr.Translate(moveDir.normalized * MoveSpeed * Time.deltaTime);
        tr.Rotate(Vector3.up * turnSpeed * Time.deltaTime * rot);
    }

    public void AttackEffect(int num)
    {
        GameObject effect = Effects[num];
        GameObject Effcet = Instantiate(effect, effectPos[0].position, Quaternion.identity);
        Destroy(Effcet, 3f);
    }
}
