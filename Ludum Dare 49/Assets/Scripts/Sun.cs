using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour {

	//############# Publically Defined Varaiables ###############
	public float burnRate = .1f;
	public float currentFuel = 3000f;
	public float maxFuel = 3000f;
	public FuelBar fuelBar;
	public Color[] sunColors = new Color[4];

	//############## Private Variables ######################
	bool isDead = false;
	float explosionShrinkRate = 10f;
	float explosionExpandRate = 100f;

	//############## Sun Components ######################
	ParticleSystem ps;
	SpriteRenderer sr;

	void Start() {
		ps = GetComponent<ParticleSystem>();
		sr = GetComponent<SpriteRenderer>();
		fuelBar.slider.maxValue = maxFuel;
	}

	void Update() {
		var main = ps.main;
		currentFuel -= burnRate * Time.deltaTime;

		Color currentColor;
		float t;
		float fuelColorRegion = maxFuel / 4;

		if (currentFuel <= 0) {
			currentColor = sunColors[3];
			if (!isDead) {
				sunDeath();
			}
		} else if (currentFuel < maxFuel / 4) { //less than 1/4
			t = currentFuel / fuelColorRegion;
			currentColor = Color.Lerp(sunColors[3], sunColors[2], t);
		} else if (currentFuel < maxFuel / 2) { //less than 1/2
			t = currentFuel % fuelColorRegion / fuelColorRegion;
			currentColor = Color.Lerp(sunColors[2], sunColors[1], t);
		} else if (currentFuel < maxFuel / 4 * 3) { //less than 3/4
			t = currentFuel % fuelColorRegion / fuelColorRegion;
			currentColor = Color.Lerp(sunColors[1], sunColors[0], t);
		} else { //full
			currentColor = sunColors[0];
		}

		sr.color = currentColor;
		main.startColor = currentColor;

		// Updates visuals
		fuelBar.UpdateFuelBar(currentFuel);
	}

	void sunDeath() {
		print("The sun ran out of fuel");
		isDead = true;
		ps.Stop();
		IEnumerator sunExplosion() {
			while (gameObject.transform.localScale.x > .1) {
				gameObject.transform.localScale -= new Vector3(explosionShrinkRate * Time.deltaTime, explosionShrinkRate * Time.deltaTime, 0);
				sr.color = Color.Lerp(sunColors[3], Color.white, gameObject.transform.localScale.x);
				yield return null;
			}
			yield return new WaitForSeconds(.5f);
			while (gameObject.transform.localScale.x < 1000) {
				gameObject.transform.localScale -= new Vector3(explosionExpandRate * Time.deltaTime, explosionExpandRate * Time.deltaTime, 0);
				yield return null;
			}
		}
		StartCoroutine(sunExplosion());
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

