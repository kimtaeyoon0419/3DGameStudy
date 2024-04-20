using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAutoTarget : MonoBehaviour
{
    [SerializeField] GameObject Player; // 플레이어
    [SerializeField] LayerMask layer; // 레이어
    [SerializeField] float radius; // 반지름
    [SerializeField] Collider[] col; // 주변 적 콜라이더
    [SerializeField] Transform target; // 최종 가까운 적

    private void Start()
    {
        InvokeRepeating(nameof(enemyAround), 0, 0.2f);
    }

    public Transform enemyAround()
    {
        col = Physics.OverlapSphere(Player.transform.position, radius, layer);
        // Player의 포지션에서 radius(반지름) 만큼 주변 layer를 검사한다.

        Transform short_enemy = null; 

        if(col.Length > 0) // 주변에 적이 있다면
        {
            float short_distance = Mathf.Infinity; // Mathf.Infinity; <- 양의 무한대

            foreach (Collider s_col  in col) // s_col 변수에 col 배열을 담아서 전부 실행
            {
                float playerToEnemy = Vector3.SqrMagnitude(Player.transform.position - s_col.transform.position);
                // 플레이어의 포지션과 s_col의 포지션의 거리를 구한다

                if(short_distance > playerToEnemy)
                {
                    short_distance = playerToEnemy;
                    short_enemy = s_col.transform;
                }
            }
        }

        target = short_enemy;

        return target;
    }
}
