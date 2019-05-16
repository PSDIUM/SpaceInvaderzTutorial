using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[Header("Physics Properties")]
	[SerializeField] private float speed = 5;
    [SerializeField] private float maxAcceleration = 5;
    [SerializeField] private float accelerationStepping = 1;
    [SerializeField] private float accelerationBase= 1;
    private float acceleration = 1;

    [Header("Projectile Details")]
    [SerializeField] private Projectile projectile;
    [SerializeField] private float shootingSpeed;
    [SerializeField] private float shotsPerSecond = 0.3f;

    private Vector2 playerBounds;
    private bool onShootCooldown;

    private void Awake() {
       
    }
    private void Start() {
        GameManager.Instance.Player = this.gameObject;
        playerBounds = GameManager.Instance.Bounds;
    }

    private void Update() {
		Movement();
        Shoot();
	}

	private void Movement() {
		float inputX = Input.GetAxis("Horizontal");
		float inputY = Input.GetAxis("Vertical");
		Vector2 velocity = new Vector2(inputX, inputY);

		velocity *= speed * acceleration * Time.deltaTime;

        Vector3 nextPosition = new Vector3(velocity.x, velocity.y, 0) + transform.position;

        nextPosition.x = Mathf.Clamp(nextPosition.x, -playerBounds.x, playerBounds.x);
        nextPosition.y = Mathf.Clamp(nextPosition.y, -playerBounds.y, playerBounds.y);

        transform.position = nextPosition;

		if (inputX!=0 || inputY!=0) {
			acceleration += accelerationStepping;
            if (acceleration >= maxAcceleration)
                acceleration = maxAcceleration; 
		} else {
			acceleration = accelerationBase;
		}
	}

    private void Shoot() {

        if (Input.GetMouseButton(0) && !onShootCooldown) {
            Projectile projectileObject = Instantiate(projectile, transform.position, Quaternion.identity);
            projectileObject.Initialise(transform, shootingSpeed);
            StartCoroutine(Cooldown(shotsPerSecond));
        }
    }

    private IEnumerator Cooldown(float time) {
        float elapsedTime = 0;
        onShootCooldown = true;

        while (elapsedTime<time) {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        onShootCooldown = false;
    }
}
