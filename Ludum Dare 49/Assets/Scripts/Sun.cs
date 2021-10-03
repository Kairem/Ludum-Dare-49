using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour {
	public float sinkSpeed = 1f;
	public float burnRate = 10f;
	public float stabilityThreshold = 1000f;
	public float currentFuel = 3000f;
	public FuelBar fuelBar;

	void Update() {
		currentFuel -= burnRate * Time.deltaTime;
		if (currentFuel < stabilityThreshold) {
			sunDeath();
		}
		//fuelBar.UpdateFuelBar(currentFuel);
	}

	void sunDeath() {
		print("The sun ran out of fuel");
	}

	void OnCollisionEnter2D(Collision2D hit) {
		if (hit.gameObject.GetComponent<SpaceShipController>()) {
			hit.gameObject.GetComponent<SpaceShipController>().TakeDamage(3);
		}
		GameObject hitGO = hit.gameObject;
		FuelObject fo = hit.gameObject.GetComponent<FuelObject>();

		//if fuel is given
		if (fo != null) {
			currentFuel += fo.fuelValue;
			//consume object
			hitGO.GetComponent<Collider2D>().enabled = false;
			Vector2 dirToCenter = (transform.position - hitGO.transform.position).normalized;
			hitGO.GetComponent<Rigidbody2D>().velocity = dirToCenter * sinkSpeed;
			hitGO.AddComponent<SunDestroy>();
		}
	}
}
