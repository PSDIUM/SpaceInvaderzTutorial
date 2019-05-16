using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    [Header("Game Properties")]
    [SerializeField] private float width = 10;
    [SerializeField] private float height = 20;

    [Header("Game Elements")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerBase;
    [SerializeField] private int score;

    public float Width { get { return width; } }
    public float Height { get { return height; } }
    public Vector2 Bounds { get { return new Vector2(width, height);  } }
    public GameObject Player { get { return player; } set { player = value; } }
    public GameObject PlayerBase { get { return playerBase; } }
    public GameObject Score { get { return playerBase; } }

    private void Awake() {
        SetSingleton();
    }
    private void SetSingleton() {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    public void GainPoints(int amount) {
        score += amount;
        UIManager.Instance.UpdateScore(score);
    }

    public void EndGame(string outcome) {
        UIManager.Instance.SetOutcome(outcome);
        Time.timeScale = 0;
    }
}
