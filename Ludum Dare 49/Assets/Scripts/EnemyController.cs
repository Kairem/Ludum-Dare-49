using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
	public Transform home;
	public Transform target;
	public float force = 20f;
	public float turnSpeed = 180f;
	public float closingDistance = 5f;
	public float rateOfFire = 5f;
	public GameObject projectilePrefab;
	public GameObject explosion;

	Rigidbody2D rb;
	Vector2 distToTarget;
	bool islaserCoooldown = false;

	void Start() {
		rb = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate() {
		distToTarget = target.position - transform.position;
		Chase();

		if (distToTarget.magnitude < closingDistance) {
			Shoot();
		}
	}

	void Shoot() {
		if (islaserCoooldown) return;
		print("SHOOTING LASER");
		islaserCoooldown = true;
		GameObject projectile = Instantiate(projectilePrefab, transform);
		projectile.transform.up = target.position - projectile.transform.position;
		StartCoroutine(LaserAttack());
	}

	IEnumerator LaserAttack() {
		yield return new WaitForSeconds(1 / rateOfFire);
		islaserCoooldown = false;
	}

	void Chase() {
		Vector2 dirToTarget = distToTarget.normalized;
		float turnDir = -Vector2.Dot(transform.right, dirToTarget);
		//print(turnDir);
		if (turnDir < 0) {
			rb.angularVelocity = -turnSpeed;
		} else {
			rb.angularVelocity = turnSpeed;
		}
		// rb.angularVelocity = turnDir * turnSpeed;
		rb.AddForce(transform.up * force);
	}

	public void Die() {
		print(gameObject.name + " was killed.");
		Instantiate(explosion, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}
