using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRandomizer : MonoBehaviour {
	public int numPlanets = 20;
	public float minSeparation = 20f;
	public float minRadius = 80f;
	public float maxRadius = 600f;
	public GasGiant gasGiant;
	public Rock rock;
	public Machine machine;
	public Habited habited;

	void populateSpace() {
		List<Vector3> planetPosList = new List<Vector3>();
		for (int i = 0; i < numPlanets; i++) {
			Vector3 position = Vector3.zero;
			bool posFound = false;
			while (!posFound) {
				posFound = true;
				//generate random position in bounds
				float dist = Random.Range(minRadius, maxRadius);
				float angle = Random.Range(0f, 360f);
				position = dist * new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f);

				foreach (Vector3 pos in planetPosList) {
					if ((pos - position).magnitude < minSeparation) {
						posFound = false;
						break;
					}
				}
			}
			//Spawn Planet
			//Determine Type
			Planet planetToSpawn;
			int rngType = Random.Range(0, 10);
			if (rngType < 4) {
				planetToSpawn = gasGiant;
			} else if (rngType < 7) {
				planetToSpawn = rock;
			} else if (rngType < 9) {
				planetToSpawn = habited;
			} else {
				planetToSpawn = machine;
			}

			//Get Parameters
			float size = Random.Range(planetToSpawn.minSize, planetToSpawn.maxSize);
			float hue = Random.Range(planetToSpawn.minHue, planetToSpawn.maxHue);
			print(hue);
			GameObject planetGO = Instantiate(planetToSpawn.planetPrefab, position, Quaternion.identity);
			planetGO.GetComponent<FuelObject>().fuelValue = planetToSpawn.fuelValue;
			planetGO.transform.localScale = new Vector3(size, size, 1);
			planetGO.GetComponent<SpriteRenderer>().color = Color.HSVToRGB(hue / 360f, .5f, 1);
			planetPosList.Add(planetGO.transform.position);
			HabitedPlanet hp = GetComponent<HabitedPlanet>();
			if (hp) {
				hp.numSoldiers = Random.Range(4, 9);
				planetGO.GetComponent<SpriteRenderer>().color = Color.HSVToRGB(hue / 360f, .2f, 1);
			}
		}
	}

	void Start() {
		populateSpace();
	}
}

public abstract class Planet {
	public float minSize, maxSize;
	public int fuelValue;
	public float minHue, maxHue;
	public GameObject planetPrefab;
}

[System.Serializable]
public class GasGiant : Planet { }

[System.Serializable]
public class Rock : Planet { }

[System.Serializable]
public class Machine : Planet { }

[System.Serializable]
public class Habited : Planet { }