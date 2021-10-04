using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChangeText : MonoBehaviour {
	public float dayChangePerSecond = 3;
	public int day = 0;
	// Start is called before the first frame update
	void Start() {
		StartCoroutine(Countdown());
	}

	// Update is called once per frame
	void Update() {
		if (!GameObject.Find("SpaceShip")) return;
		if (GetComponent<TextMeshProUGUI>()) {
			GetComponent<TextMeshProUGUI>().text = "Month: " + day.ToString();
		}

	}

	private IEnumerator Countdown() {
		float duration = dayChangePerSecond;
		float normalizedTime = 0;
		while (normalizedTime <= 1f) {
			// if leave early exit early.
			normalizedTime += Time.deltaTime / duration;
			yield return null;
		}
		day += 1;
		StartCoroutine(Countdown());
	}
}
