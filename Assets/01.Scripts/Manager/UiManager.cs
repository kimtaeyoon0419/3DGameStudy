namespace AutoBattle.Manager
{
    // # System
    using System.Collections;
    using System.Collections.Generic;

    // # Unity
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class UiManager : MonoBehaviour
    {
        [SerializeField] GameObject BlueWin;
        [SerializeField] GameObject RedWin;

        private void Update()
        {
            WinnerBanner();
        }

        private void WinnerBanner()
        {
            if (GameManager.instance.state == GameState.BlueWin)
            {
                BlueWin.GetComponent<Animator>().SetBool("Winner", true);
            }
            if (GameManager.instance.state == GameState.RedWin)
            {
                RedWin.GetComponent<Animator>().SetBool("Winner", true);
            }
        }
        public void StartBtn()
        {
            GameManager.instance.StartGame();
        }
        public void ReStart()
        {
            SceneManager.LoadScene("Main");
        }
    }
}
