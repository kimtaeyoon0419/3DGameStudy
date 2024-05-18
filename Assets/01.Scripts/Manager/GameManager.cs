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
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
