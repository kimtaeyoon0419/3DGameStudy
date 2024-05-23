namespace AutoBattle.Charater
{
    // # System
    using System.Collections;
    using System.Collections.Generic;
    using Unity.VisualScripting;

    // # Unity
    using UnityEngine;
    using UnityEngine.AI;
    using UnityEngine.PlayerLoop;
    using UnityEngine.UI;

    [RequireComponent(typeof(Rigidbody))]
    public class Character : MonoBehaviour
    {
        #region enum
        public enum Team
        {
            blue,
            red
        }
        public enum CharState
        {
            Idle,
            Trace,
            IsAttack,
            Die,
            Winner
        }
        #endregion

        [Header("State")]
        [SerializeField] public CharState state;
        [SerializeField] public Team team;
        [SerializeField] protected float speed;
        [SerializeField] protected GameObject curEnemy;
        [SerializeField] protected float enemyDistance;
        private protected bool isDie;
        private bool isFindEnemyCo = true;

        [Header("Stat")]
        [SerializeField] protected float attackRange;
        [SerializeField] protected float maxAttackSpeed;
        [SerializeField] protected float curAttackSpeed;
        [SerializeField] protected int damage;
        [SerializeField] protected int maxHp;
        [SerializeField] protected int curHp;

        [Header("NavMeshAgent")]
        [SerializeField] protected NavMeshAgent nmAgent;

        [Header("Animation")]
        Animator animator;
        protected readonly int hashIsRun = Animator.StringToHash("IsRun");
        protected readonly int hashAttack = Animator.StringToHash("Attack");
        protected readonly int hashDeath = Animator.StringToHash("Death");

        [Header("Effect")]
        [SerializeField] protected GameObject dieEffect;

        #region Unity_Function

        private void Awake()
        {
            nmAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
        }

        protected virtual void Start()
        {
            if (team == Team.blue)
            {
                GameManager.instance.blueUnits.Add(gameObject);
            }
            else if (team == Team.red)
            {
                GameManager.instance.redUnits.Add(gameObject);
            }
            state = CharState.Idle;
            StartCoroutine(Co_FindEnemy());
            curHp = maxHp;
        }

        protected virtual void Update()
        {
            curAttackSpeed -= Time.deltaTime; // 공격속도 돌리기
            CheckState();
            ChangeState();

            if (isFindEnemyCo == false)
            {
                StartCoroutine(Co_FindEnemy());
            }
        }

        private void LateUpdate()
        {
            if(state != CharState.Die && isFindEnemyCo == true && curEnemy == null)
            {
                StopCoroutine(Co_FindEnemy());
                StartCoroutine(Co_FindEnemy());
            }
        }
        #endregion

        #region Private_Function
        private void CheckState()
        {
            switch (state)
            {
                case CharState.Trace:
                    animator.SetBool(hashIsRun, true);
                    FollowEnemy();
                    break;
                case CharState.IsAttack:
                    animator.SetBool(hashIsRun, false);
                    if (curAttackSpeed <= 0)
                    {
                        Attack();
                    }
                    break;
                case CharState.Die:
                    if (!isDie)
                    {
                        isDie= true;
                        nmAgent.speed = 0;
                        StopAllCoroutines();
                        animator.SetTrigger(hashDeath);
                        StartCoroutine(Co_DeathAnim());
                    }
                    break;
                case CharState.Winner:
                    animator.SetBool(hashIsRun, false);
                    break;
            }
        }

        /// <summary>
        /// 현재 상황에 맞는 상태로 전환시켜줌
        /// </summary>
        private void ChangeState()
        {
            if (curEnemy != null && GameManager.instance.state == GameState.Battle)
            {
                enemyDistance = Vector3.Distance(curEnemy.transform.position, transform.position);

                if (enemyDistance <= attackRange) // 공격 범위 안에 적이 들어왔을 때
                {
                    state = CharState.IsAttack;
                }
                if (enemyDistance > attackRange) // 공격 범위 밖으로 적이 나갔을 때
                {
                    state = CharState.Trace;
                }
                if (curHp <= 0) // 체력이 0 이거나 아래로 내려갔을 때
                {
                    if (team == Team.red) // 레드팀이라면
                    {
                        GameManager.instance.redUnits.Remove(gameObject);
                    }
                    if (team == Team.blue) // 블루팀이라면
                    {
                        GameManager.instance.blueUnits.Remove(gameObject);
                    }
                    state = CharState.Die;
                }
                if (GameManager.instance.state == GameState.BlueWin) // 블루팀이 승리했을 때
                {
                    if (team == Team.blue) // 블루팀이라면
                    {
                        state = CharState.Winner;
                    }
                }
                if (GameManager.instance.state == GameState.RedWin) // 레드팀이 승리했을 때
                {
                    if (team == Team.red) // 레드팀이라면
                    {
                        state = CharState.Winner;
                    }
                }
            }
        }

        /// <summary>
        /// 적 따라가기
        /// </summary>
        private void FollowEnemy()
        {
            if (curEnemy != null)
            {
                nmAgent.SetDestination(curEnemy.transform.position);
            }
        }
        protected virtual void Attack()
        {
            if (state != CharState.Die)
            {
                animator.SetTrigger(hashAttack);
                curAttackSpeed =  maxAttackSpeed;
            }
        }
       
        #endregion

        #region Public_Function
        public void HitAttack()
        {
            if (curEnemy != null)
            {
                curEnemy.GetComponent<Character>().TakeDamage(damage);
            }
        }
        public void TakeDamage(int damage)
        {
            curHp -= damage;
        }
        #endregion

        #region Coroutine_Function
        /// <summary>
        /// 적 찾기
        /// </summary>
        IEnumerator Co_FindEnemy()
        {
            isFindEnemyCo = true;
            yield return null;
            float curDistanceToTarger = float.MaxValue;
            
            if (team == Team.blue)
            {
                foreach (GameObject enemy in GameManager.instance.redUnits)
                {
                    float distanceToTarger = Vector2.Distance(enemy.transform.position, transform.position);
                    if (distanceToTarger <= curDistanceToTarger)
                    {
                        curEnemy = enemy;

                        curDistanceToTarger = distanceToTarger;
                    }
                }
            }
            if (team == Team.red)
            {
                foreach (GameObject enemy in GameManager.instance.blueUnits)
                {
                    float distanceToTarger = Vector2.Distance(enemy.transform.position, transform.position);
                    if (distanceToTarger <= curDistanceToTarger)
                    {
                        curEnemy = enemy;

                        curDistanceToTarger = distanceToTarger;
                    }
                }
            }
            yield return new WaitForSeconds(2f);
            yield return isFindEnemyCo = false;
            if (curEnemy == null || enemyDistance > attackRange)
            {
                StartCoroutine(Co_FindEnemy());
            }
        }

        IEnumerator Co_DeathAnim()
        {
            yield return new WaitForSeconds(0.01f);
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
            Instantiate(dieEffect, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        #endregion
    }
}