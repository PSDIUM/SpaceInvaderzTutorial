using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    private static InputManager _instance;
    public static InputManager Instance { get { return _instance; } }

    private void Awake() {
        SetSingleton();
    }
    private void SetSingleton() {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
        }
        else {
            _instance = this;
        }
    }

    public Vector3 GetMouseWorldPosition() {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

}
