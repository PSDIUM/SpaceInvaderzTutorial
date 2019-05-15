using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[Header("Physics Properties")]
	[SerializeField] private float speed = 5;
	[SerializeField] private float acceleration = 1;

	private void Update() {
		Movement();

	}

	private void Movement() {
		float inputX = Input.GetAxis("Horizontal");
		float inputY = Input.GetAxis("Vertical");
		Vector2 velocity = new Vector2(inputX, inputY);

		velocity *= speed * Time.deltaTime;

		transform.position += new Vector3(velocity.x, velocity.y, 0);

		if (inputX>0 || inputY>0) {
			acceleration += 1 *Time.deltaTime;
		} else {
			acceleration = 1;
		}
	}

	private void Acceleration() {

	}
}
