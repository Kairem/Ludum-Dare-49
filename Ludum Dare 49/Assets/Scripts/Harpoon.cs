using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harpoon : MonoBehaviour {
	public float harpoonSpeed = 5f;
	public float maxDistance = 10;
	Rigidbody2D rb;
	bool hasHit = false;
	public MusicManager musicManager;

	void Start() {
		rb = gameObject.GetComponent<Rigidbody2D>();
		// rb.AddForce(new Vector2(0, barbForce), ForceMode2D.Impulse);
		rb.velocity = gameObject.transform.up * harpoonSpeed;
	}

	void Update() {
		if (!hasHit) {
			if ((transform.parent.position - transform.position).magnitude >= maxDistance) {
				rb.velocity = Vector2.zero;
				GetComponent<Collider2D>().isTrigger = false;
				hasHit = true;
				DistanceJoint2D joint = transform.parent.gameObject.GetComponent<DistanceJoint2D>();
				joint.autoConfigureConnectedAnchor = true;
				joint.autoConfigureDistance = false;
				joint.distance = maxDistance;
				joint.connectedBody = rb;
				joint.maxDistanceOnly = true;
				joint.enableCollision = true;
				joint.enabled = true;
			}
		}
	}
	void OnTriggerEnter2D(Collider2D hit) {
		if (hasHit) return;
		GameObject go = hit.gameObject;


		if (go.GetComponent<FuelObject>()) {
			hasHit = true;
			rb.velocity = Vector2.zero;
			Rigidbody2D hitRB = hit.gameObject.GetComponent<Rigidbody2D>();
			DistanceJoint2D joint = transform.parent.gameObject.GetComponent<DistanceJoint2D>();
			joint.autoConfigureConnectedAnchor = true;
			joint.autoConfigureDistance = false;
			joint.distance = maxDistance;
			joint.connectedBody = hitRB;
			joint.maxDistanceOnly = true;

			FixedJoint2D fixedJoint = gameObject.AddComponent<FixedJoint2D>();
			fixedJoint.connectedBody = hitRB;
			joint.enableCollision = false;

			joint.enableCollision = true;
			joint.enabled = true;

			if (go.GetComponent<HabitedPlanet>()) {
				go.GetComponent<HabitedPlanet>().DispatchSoldiers(transform.parent.gameObject);
				musicManager.PlayDeathSoundtrack();
			}

		} else if (go.GetComponent<EnemyController>()) {
			go.GetComponent<EnemyController>().Die();
		}
	}
}
