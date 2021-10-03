using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour {

	public float bulletSpeed = 10f;

	Rigidbody2D rb;
	void Start() {
		rb = GetComponent<Rigidbody2D>();
		rb.velocity = transform.up * bulletSpeed;
		StartCoroutine(DelayDestroy());
	}

	IEnumerator DelayDestroy() {
		yield return new WaitForSeconds(3);
		Destroy(gameObject);
	}

	void OnTriggerEnter2D(Collider2D hit) {
		GameObject hitGO = hit.gameObject;
		print(hitGO);
		if (hitGO.GetComponent<SpaceShipController>()) {
			print("Laser Hit: 1 Damage");
		}
		Destroy(gameObject);
	}
}
