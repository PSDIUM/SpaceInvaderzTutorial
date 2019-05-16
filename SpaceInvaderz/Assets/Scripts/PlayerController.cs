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

	private void Update() {
		Movement();
	}

	private void Movement() {
		float inputX = Input.GetAxis("Horizontal");
		float inputY = Input.GetAxis("Vertical");
		Vector2 velocity = new Vector2(inputX, inputY);

		velocity *= speed * acceleration * Time.deltaTime;

		transform.position += new Vector3(velocity.x, velocity.y, 0);

		if (inputX!=0 || inputY!=0) {
			acceleration += accelerationStepping;
            if (acceleration >= maxAcceleration)
                acceleration = maxAcceleration; 
		} else {
			acceleration = accelerationBase;
		}
	}
}
