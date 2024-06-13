// # System
using AutoBattle;
using System.Collections;
using System.Collections.Generic;

// # Unity
using UnityEngine;

public class WinnerBoom : MonoBehaviour
{
    [Header("BoomPos")]
    public Transform[] redBoom;
    public Transform[] blueBoom;

    [Header("VFX")]
    public GameObject boomVfx;

    private bool isBoom = false;

    private void Update()
    {
        if (GameManager.instance.state == GameState.BlueWin && !isBoom)
        {
            isBoom = true;
            StartCoroutine(Co_WinnerBoom(0));
        }
        if (GameManager.instance.state == GameState.RedWin && !isBoom)
        {
            isBoom = true;
            StartCoroutine(Co_WinnerBoom(1));
        }
    }

    /// <summary>
    /// ÀÌ±äÆÀ¿¡ ÆøÁ×
    /// </summary>
    /// <param name="winner">0 : blueTeam Winner / 1 : RedTeam Winner</param>
    /// <returns></returns>
    IEnumerator Co_WinnerBoom(int winner)
    {
        if (winner == 0)
        {
            for (int i = 0; i < blueBoom.Length; i++)
            {
                yield return new WaitForSeconds(0.5f);
                Instantiate(boomVfx, blueBoom[i].position, Quaternion.identity);
            }
        }
        if (winner == 1)
        {
            for (int i = 0; i < redBoom.Length; i++)
            {
                yield return new WaitForSeconds(0.5f);
                Instantiate(boomVfx, redBoom[i].position, Quaternion.identity);
            }
        }
        else
        {
            yield return null;
        }
    }
}
