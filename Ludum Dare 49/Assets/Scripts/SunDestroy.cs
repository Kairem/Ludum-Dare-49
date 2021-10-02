using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunDestroy : MonoBehaviour {
	Vector3 sunPos;

	void Start() {
		sunPos = GameObject.Find("Sun").transform.position;
	}

	void Update() {
		if ((sunPos - transform.position).magnitude < 1) {
			Destroy(gameObject);
		}
	}


}
