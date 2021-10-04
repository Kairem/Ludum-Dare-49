using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunDestroy : MonoBehaviour {
	Vector3 sunPos;
	public float sinkSpeed = 1f;
	Vector2 dirToCenter;

	Rigidbody2D rb;
	void Start() {
		rb = GetComponent<Rigidbody2D>();
		sunPos = GameObject.Find("Sun").transform.position;
		GetComponent<Collider2D>().enabled = false;
		dirToCenter = (sunPos - transform.position).normalized;
		rb.velocity = dirToCenter * sinkSpeed;
	}

	void Update() {
		rb.velocity = dirToCenter * sinkSpeed;
		if ((sunPos - transform.position).magnitude < 1) {
			Destroy(gameObject);
		}
	}


}
