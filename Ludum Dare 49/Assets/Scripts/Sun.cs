using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour {
	public float burnRate = 10f;
	public float currentFuel = 3000f;
	public FuelBar fuelBar;
	public Color[] sunColors = new Color[4];

	ParticleSystem ps;

	void Start() {
		ps = GetComponent<ParticleSystem>();
	}

	void Update() {
		currentFuel -= burnRate * Time.deltaTime;
		if (currentFuel < 3000 * 0.75) {
			GetComponent<SpriteRenderer>().color = sunColors[0];
		} else if (currentFuel < 3000 * 0.50) {
			//Yellow
			GetComponent<SpriteRenderer>().color = sunColors[1];
		} else if (currentFuel < 3000 * 0.25) {
			//Red
			GetComponent<SpriteRenderer>().color = sunColors[2];
		} else if (currentFuel <= 0) {
			//Explode;
			sunDeath();
		} else {
			GetComponent<SpriteRenderer>().color = sunColors[3];
			// Updates visuals
			fuelBar.UpdateFuelBar(currentFuel);
		}
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
			hitGO.AddComponent<SunDestroy>();
		} else if (hitGO.GetComponent<EnemyController>()) {
			hitGO.GetComponent<EnemyController>().Die();
		}
	}
}

