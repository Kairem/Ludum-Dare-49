using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barb : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D hit) {
		GameObject go = hit.gameObject;
		print("HIT");
		if (go.GetComponent<FuelObject>()) {
			HingeJoint2D joint = go.AddComponent<HingeJoint2D>();
			joint.connectedBody = gameObject.GetComponent<Rigidbody2D>();

		}
	}
}
