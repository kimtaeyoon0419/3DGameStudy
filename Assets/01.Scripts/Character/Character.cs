// # System
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

// # Unity
using UnityEngine;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(Rigidbody))]
public class Character : MonoBehaviour
{
    #region enum
    protected enum Team
    {
        blue,
        red
    }
    protected enum State
    {
        Idle,
        Fight,
        Die
    }
    #endregion

    [SerializeField] protected Team team;
    [SerializeField] protected State state;
    [SerializeField] protected float speed;
    [SerializeField] protected GameObject curEnemy;

    #region Unity_Function
    private void Start()
    {
        if (team == Team.blue)
        {
            GameManager.instance.blueUnits.Add(gameObject);
        }
        else if (team == Team.red)
        {
            GameManager.instance.redUnits.Add(gameObject);
        }
        StartCoroutine(Co_FindEnemy());
    }

    private void Update()
    {
        CheckState();
    }
    #endregion

    #region Private_Function
    private void CheckState()
    {
        switch (state)
        {
            case State.Fight:
                FollowEnemy();
                break;
            case State.Die:
                break;
        }
    }

    private void FollowEnemy()
    {
        transform.position = Vector3.Lerp(transform.position, curEnemy.transform.position, Time.deltaTime *speed);
        transform.LookAt(curEnemy.transform.position);
    }
    #endregion

    #region Coroutine_Function
    IEnumerator Co_FindEnemy()
    {
        yield return null;
        Debug.Log("적을 찾습니다");
        float curDistanceToTarger = float.MaxValue;
        if (team == Team.blue)
        {
            foreach (GameObject enemy in GameManager.instance.redUnits)
            {
                float distanceToTarger = Vector2.Distance(enemy.transform.position, transform.position);
                if (distanceToTarger <= curDistanceToTarger)
                {
                    Debug.Log("적을 찾았다!");
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
                    Debug.Log("적을 찾았다!");
                    curEnemy = enemy;

                    curDistanceToTarger = distanceToTarger;
                }
            }
        }
        yield return new WaitForSeconds(5f);
        yield return StartCoroutine(Co_FindEnemy());
    }
    #endregion
}
