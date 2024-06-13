namespace AutoBattle.UI
{
    // # System
    using System.Collections;
    using System.Collections.Generic;
    using TMPro;

    // # Unity
    using UnityEngine;

    public class GameStateTextMove : MonoBehaviour
    {
        private Animator animator;
        private readonly int hashGetReady = Animator.StringToHash("GetReady");
        private readonly int hashFight = Animator.StringToHash("Fight");
        [SerializeField] TextMeshProUGUI tmp;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (GameManager.instance.state == GameState.GetReady)
            {
                animator.SetBool(hashGetReady, true);
            }
            else if(GameManager.instance.state == GameState.Battle)
            {
                animator.SetBool(hashGetReady, false);
                animator.SetBool(hashFight, true);
                tmp.text = "Fight!";
                tmp.color = Color.red;
            }
            else if(GameManager.instance.state == GameState.RedWin)
            {
                animator.SetBool(hashFight,false);
                tmp.text = "RedWin";
            }
            else if(GameManager.instance.state == GameState.BlueWin)
            {
                animator.SetBool(hashFight, false);
                tmp.text = "BlueWin";
                tmp.color = Color.blue;
            }
    }
    }
}
