using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedDestroy : MonoBehaviour {
	public float destoryTimer = 2f;

	void Start() {
		StartCoroutine(DestoryAfterSeconds());
	}

	IEnumerator DestoryAfterSeconds() {
		yield return new WaitForSeconds(destoryTimer);
		Destroy(gameObject);
	}
}
