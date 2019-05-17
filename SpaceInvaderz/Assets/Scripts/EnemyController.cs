using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : ShipController {

    [Header("Enemy Properties")]
    [SerializeField] private float speed = 5;
    [SerializeField] private int pointValue;

	[Header("Enemy Elements")]
    [SerializeField] private GameObject target;

	protected override void OnStart() {
		currentHealth = maxHealth;
        SetTarget();
    }

    private void SetTarget() {
        target = GameManager.Instance.Player;
    }

    protected override void OnUpdate() {
        FollowTarget();
		Rotation();
		Shoot();
    }

    private void FollowTarget() {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
    }

	protected override void Rotation() {
		targetPos = target.transform.position;
		base.Rotation();
	}

	private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag.Equals("PlayerProjectile")) {
            Damage();
            Destroy(col.gameObject);
        }
    }

	protected override void Shoot() {
		if (!onShootCooldown) {
			Projectile projectileObject = Instantiate(projectile, transform.position, transform.rotation);
			Vector2 dir = target.transform.position - transform.position;
			projectileObject.Initialise(dir.normalized, shootingSpeed);
			StartCoroutine(Cooldown(shotsPerSecond));
		}
	}

	private void Damage() {
        if (currentHealth>1) {
            currentHealth--;
        } else {
			GameManager.Instance.DestroyEnemy(pointValue);
            Destroy(this.gameObject);
        }
    }
}
