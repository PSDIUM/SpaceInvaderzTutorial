using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    [Header("Physics Details")]
    [SerializeField] private float speed = 5;

    private Vector2 dir;

    public void Initialise(Transform player) {
        dir = InputManager.Instance.GetMouseWorldPosition() - player.position;
        dir.Normalize();
    }

    private void Update() {
        Movement();
    }

    private void Movement() {
        Vector2 velocity = dir * speed * Time.deltaTime;
        transform.position += new Vector3(velocity.x, velocity.y, 0);
    }

}
