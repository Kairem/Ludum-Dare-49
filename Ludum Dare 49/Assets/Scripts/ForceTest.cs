using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceTest : MonoBehaviour {
	// Start is called before the first frame update
	void Start() {
		gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 1f));
		//StartCoroutine(forces());
	}

	IEnumerator forces() {
		gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 1f));
		yield return new WaitForSeconds(2f);
		gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, -1f));
	}

	// Update is called once per frame
	void Update() {
		print(gameObject.GetComponent<Rigidbody2D>().velocity);
	}
}
