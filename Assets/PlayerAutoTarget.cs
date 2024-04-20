using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAutoTarget : MonoBehaviour
{
    [SerializeField] GameObject Player; // �÷��̾�
    [SerializeField] LayerMask layer; // ���̾�
    [SerializeField] float radius; // ������
    [SerializeField] Collider[] col; // �ֺ� �� �ݶ��̴�
    [SerializeField] Transform target; // ���� ����� ��

    private void Start()
    {
        InvokeRepeating(nameof(enemyAround), 0, 0.2f);
    }

    public Transform enemyAround()
    {
        col = Physics.OverlapSphere(Player.transform.position, radius, layer);
        // Player�� �����ǿ��� radius(������) ��ŭ �ֺ� layer�� �˻��Ѵ�.

        Transform short_enemy = null; 

        if(col.Length > 0) // �ֺ��� ���� �ִٸ�
        {
            float short_distance = Mathf.Infinity; // Mathf.Infinity; <- ���� ���Ѵ�

            foreach (Collider s_col  in col) // s_col ������ col �迭�� ��Ƽ� ���� ����
            {
                float playerToEnemy = Vector3.SqrMagnitude(Player.transform.position - s_col.transform.position);
                // �÷��̾��� �����ǰ� s_col�� �������� �Ÿ��� ���Ѵ�

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
