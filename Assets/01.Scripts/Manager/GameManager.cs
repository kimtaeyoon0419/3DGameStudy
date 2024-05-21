// # System
using System.Collections;
using System.Collections.Generic;

// # Unity
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Winner")]
    public bool redWin;
    public bool blueWin;

    [Header("TeamList")]
    public List<GameObject> redUnits = new List<GameObject>();
    public List<GameObject> blueUnits = new List<GameObject>();

    [Header("VFX")]
    public GameObject boomVfx;
    public Transform[] redBoom;
    public Transform[] blueBoom;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        if (!blueWin && !redWin)
        {
            CheckWinner();
        }
    }

    #region Private_Function
    private void CheckWinner()
    {
        if (redUnits.Count <= 0)
        {
            blueWin = true;
            Debug.Log("ºí·çÆÀÀÌ ½Â¸®Çß½À´Ï´Ù!");
            StartCoroutine(Co_WinnerBoom());
        }
        if (blueUnits.Count <= 0)
        {
            redWin = true;
            Debug.Log("·¹µåÆÀÀÌ ½Â¸®Çß½À´Ï´Ù!");
            StartCoroutine(Co_WinnerBoom());
        }
    }
    #endregion

    #region Coroutine_Function
    IEnumerator Co_WinnerBoom()
    {
        if(blueWin)
        {
            for (int i = 0; i < blueBoom.Length; i++)
            {
                yield return new WaitForSeconds(0.5f);
                Instantiate(boomVfx, blueBoom[i].position, Quaternion.identity);
            }
        }
        if (redWin)
        {
            for (int i = 0; i < redBoom.Length; i++)
            {
                yield return new WaitForSeconds(0.5f);
                Instantiate(boomVfx, redBoom[i].position, Quaternion.identity);
            }
        }
    }
    #endregion
}
