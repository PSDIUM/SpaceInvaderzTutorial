using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour {

    private static UIManager _instance;
    public static UIManager Instance { get { return _instance; } }

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI outcome;

    private void Awake() {
        SetSingleton();
    }

    private void Start() {
        outcome.enabled = false;
        
    }
    private void SetSingleton() {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
        }
        else {
            _instance = this;
        }
    }

    public void UpdateScore(int amount) {
        score.text = "Score: " + amount;
    }

    public void SetOutcome(string value) {
        outcome.text = value;
        outcome.enabled = true;
    }

}
