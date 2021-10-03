using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabitedPlanet : MonoBehaviour {

	public GameObject soldierPrefab;
	public int numSoldiers = 5;
	public float spawnPadding = 1f;

	bool hasDispatched = false;

	public void DispatchSoldiers(GameObject target) {
		print("Habited hit");
		if (hasDispatched) return;
		hasDispatched = true;
		for (int i = 0; i < numSoldiers; i++) {
			float rng = Random.Range(0f, 6.28f);
			Vector3 spawnPos = transform.position + ((transform.lossyScale.x + spawnPadding) * new Vector3(Mathf.Cos(rng), Mathf.Sin(rng), 0));
			Instantiate(soldierPrefab, spawnPos, Quaternion.identity).GetComponent<EnemyController>().target = target.transform;
		}
	}
}
