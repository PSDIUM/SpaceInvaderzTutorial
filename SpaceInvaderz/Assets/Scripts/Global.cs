using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Global {

	public static Quaternion LookTowards(Vector3 target, Vector3 source) {
		Vector2 dir = target - source;
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		return Quaternion.AngleAxis(angle - 90, Vector3.forward);
	}
}
