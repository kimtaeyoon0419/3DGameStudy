namespace AutoBattle
{
    using AutoBattle.Charater;
    // # System
    using System.Collections;
    using System.Collections.Generic;

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
                StartCoroutine(Co_WinnerBoom(0));
            }
            if (blueUnits.Count <= 0)
            {
                state = GameState.RedWin;
                Debug.Log("·¹µåÆÀÀÌ ½Â¸®Çß½À´Ï´Ù!");
                StartCoroutine(Co_WinnerBoom(1));
            }
        }
        #endregion

        #region Public_Function
        public void StartGame()
        {
            state = GameState.Battle;
            for(int i = 0; i < blueUnits.Count; i++)
            {
                blueUnits[i].GetComponent<Character>().state = Character.CharState.Trace;
            }
            for (int i = 0; i < redUnits.Count; i++)
            {
                redUnits[i].GetComponent<Character>().state = Character.CharState.Trace;
            }
        }
        #endregion

        #region Coroutine_Function
        /// <summary>
        /// ÀÌ±äÆÀ¿¡ ÆøÁ×
        /// </summary>
        /// <param name="winner">0Àº blueteam 1Àº redteam</param>
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
        #endregion
    }
}
