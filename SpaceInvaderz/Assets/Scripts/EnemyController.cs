using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    [Header("Enemy Properties")]
    [SerializeField] private int health = 3;
    [SerializeField] private float speed = 5;
    [SerializeField] private int pointValue;
	[SerializeField] private float projectileSpeed = 20;
	[SerializeField] private float shotsPerSecond = 0.3f;

	[Header("Enemy Elements")]
    [SerializeField] private GameObject target;
	[SerializeField] private Projectile projectile;
	private bool onShootCooldown;

	private void Start() {
        SetTarget();
    }

    private void SetTarget() {
        target = GameManager.Instance.Player;
    }

    private void Update() {
        FollowTarget();
		Rotation();
		Shoot();
    }

    private void FollowTarget() {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
    }

	private void Rotation() {
		transform.rotation = Global.LookTowards(target.transform.position, transform.position);
	}

	private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag.Equals("PlayerProjectile")) {
            Damage();
            Destroy(col.gameObject);
        }
    }

	private void Shoot() {
		if (!onShootCooldown) {
			Projectile projectileObject = Instantiate(projectile, transform.position, transform.rotation);
			Vector2 dir = target.transform.position - transform.position;
			projectileObject.Initialise(dir.normalized, projectileSpeed);
			StartCoroutine(Cooldown(shotsPerSecond));
		}
	}

	private IEnumerator Cooldown(float time) {
		float elapsedTime = 0;
		onShootCooldown = true;

		while (elapsedTime < time) {
			elapsedTime += Time.deltaTime;
			yield return null;
		}

		onShootCooldown = false;
	}


	private void Damage() {
        if (health>1) {
            health--;
        } else {
			GameManager.Instance.DestroyEnemy(pointValue);
            Destroy(this.gameObject);
        }
    }
}
