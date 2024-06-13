namespace AutoBattle
{
    using AutoBattle.Charater;
    // # System
    using System.Collections;
    using System.Collections.Generic;
    using System.Security.Cryptography;

    // # Unity
    using UnityEngine;
    public enum GameState
    {
        GetReady,
        Battle,
        RedWin,
        BlueWin
    }

    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        [Header("GameState")]
        public GameState state;

        [Header("TeamList")]
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

        private void Start()
        {
            state = GameState.GetReady;
        }

        private void Update()
        {
            if (state != GameState.BlueWin && state != GameState.RedWin && state == GameState.Battle)
            {
                CheckWinner();
            }
        }

        #region Private_Function
        private void CheckWinner()
        {
            if (redUnits.Count <= 0)
            {
                state = GameState.BlueWin;
                Debug.Log("ºí·çÆÀÀÌ ½Â¸®Çß½À´Ï´Ù!");
            }
            if (blueUnits.Count <= 0)
            {
                state = GameState.RedWin;
                Debug.Log("·¹µåÆÀÀÌ ½Â¸®Çß½À´Ï´Ù!");
            }
        }
        #endregion

        #region Public_Function
        public void StartGame()
        {
            state = GameState.Battle;
            for (int i = 0; i < blueUnits.Count; i++)
            {
                blueUnits[i].GetComponent<Character>().state = Character.CharState.Trace;
            }
            for (int i = 0; i < redUnits.Count; i++)
            {
                redUnits[i].GetComponent<Character>().state = Character.CharState.Trace;
            }
        }

        public void ReStartGame()
        {
            redUnits.Clear();
            blueUnits.Clear();
            state = GameState.GetReady;
        }
        #endregion
    }
}
