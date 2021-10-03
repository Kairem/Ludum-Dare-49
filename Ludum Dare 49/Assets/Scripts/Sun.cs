using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour {
	public float sinkSpeed = 1f;
	public float burnRate = 10f;
	public float currentFuel = 3000f;
	public FuelBar fuelBar;

	void Update() {
		currentFuel -= burnRate * Time.deltaTime;
		if (currentFuel < 3000 * 0.75)
        {
			//Green;
			//ParticleSystem.MainModule settings = GetComponent<ParticleSystem>().main;
			//settings.startColor = new ParticleSystem.MinMaxGradient(new Color(41, 241, 195, 0.75f));

			GetComponent<SpriteRenderer>().color = new Color(41, 241, 195, 0.75f);
		} else if (currentFuel < 3000 * 0.50)
        {
			//Yellow
			GetComponent<SpriteRenderer>().color = new Color(255, 227, 107, 255);
		}
		else if (currentFuel < 3000 * 0.25)
        {
			//Red
			GetComponent<SpriteRenderer>().color = new Color(218, 43, 39, 255);
		}
		else if (currentFuel <= 0)
        {
			//Explode;
			sunDeath();
		} else
        {
			GetComponent<SpriteRenderer>().color = new Color(27, 3, 163, 255);
		}
		// Updates visuals
		fuelBar.UpdateFuelBar(currentFuel);

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
