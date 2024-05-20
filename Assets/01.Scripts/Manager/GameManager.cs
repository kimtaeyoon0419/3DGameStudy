// # System
using System.Collections;
using System.Collections.Generic;

// # Unity
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool redWin;
    public bool blueWin;

    public List<GameObject> redUnits = new List<GameObject>();

    public List<GameObject> blueUnits = new List<GameObject>();

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
        CheckWinner();
    }

    #region Private_Function
    private void CheckWinner()
    {
        if (redUnits.Count <= 0)
        {
           blueWin = true;
            Debug.Log("ºí·çÆÀÀÌ ½Â¸®Çß½À´Ï´Ù!");
        }
        if(blueUnits.Count <= 0)
        {
            redWin = true;
            Debug.Log("·¹µåÆÀÀÌ ½Â¸®Çß½À´Ï´Ù!");
        }
    }
    #endregion
}
