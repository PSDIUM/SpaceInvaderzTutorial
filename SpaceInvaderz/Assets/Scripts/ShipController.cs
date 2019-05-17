using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour {

	[Header("Ship Properties")]
	[SerializeField] protected int maxHealth = 3;
	protected int currentHealth;
	protected Vector3 targetPos;
	protected bool onShootCooldown;

	[Header("Projectile Details")]
	[SerializeField] protected Projectile projectile;
	[SerializeField] protected float shootingSpeed = 20;
	[SerializeField] protected float shotsPerSecond = 0.3f;

	private void Awake() {
		OnAwake();
	}

	private void Start() {
		OnStart();
	}

	public virtual void Update() {
		OnUpdate();
	}

	protected virtual void OnAwake() { }

	protected virtual void OnStart() { }

	protected virtual void OnUpdate() { }

	protected virtual void Shoot() { }

	protected virtual void Rotation() {
		transform.rotation = Global.LookTowards(targetPos, transform.position);
	}

	protected IEnumerator Cooldown(float time) {
		float elapsedTime = 0;
		onShootCooldown = true;

		while (elapsedTime < time) {
			elapsedTime += Time.deltaTime;
			yield return null;
		}

		onShootCooldown = false;
	}
}
