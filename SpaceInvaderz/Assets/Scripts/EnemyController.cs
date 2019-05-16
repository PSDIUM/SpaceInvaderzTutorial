using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    [Header("Enemy Properties")]
    [SerializeField] private int health = 3;
    [SerializeField] private float speed = 5;
    [SerializeField] private int pointValue;
    [SerializeField] private GameObject target;

    private void Start() {
        SetTarget();
    }

    private void SetTarget() {
        target = GameManager.Instance.PlayerBase;
    }

    private void Update() {
        FollowTarget();
    }

    private void FollowTarget() {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag.Equals("Projectile")) {
            Damage();
            Destroy(col.gameObject);
        }
    }

    private void Damage() {
        if (health>1) {
            health--;
        } else {
            GameManager.Instance.GainPoints(pointValue);
            Destroy(this.gameObject);
        }
    }
}
