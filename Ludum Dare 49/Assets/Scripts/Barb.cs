using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barb : MonoBehaviour {
	public float barbForce = 5f;
	Rigidbody2D rb;
	void Start() {
		rb = gameObject.GetComponent<Rigidbody2D>();
		// rb.AddForce(new Vector2(0, barbForce), ForceMode2D.Impulse);
		rb.velocity = new Vector2(0, 150);
	}
	void OnTriggerEnter2D(Collider2D hit) {
		GameObject go = hit.gameObject;
		print("HIT");
		if (go.GetComponent<FuelObject>()) {
			HingeJoint2D joint = go.AddComponent<HingeJoint2D>();
			joint.connectedBody = rb;

		}
	}
}
