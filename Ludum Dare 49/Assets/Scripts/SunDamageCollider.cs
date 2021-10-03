using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunDamageCollider : MonoBehaviour
{
	bool takeDamage;
	public SpaceShipController spaceShip;
	public GameObject warningDisplay;

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.GetComponent<SpaceShipController>()) {
			//Display thingy
			if (warningDisplay != null)
			{
				warningDisplay.SetActive(true);
			}
			takeDamage = true;
			StartCoroutine(Countdown());
		}
		
	}

	private void OnTriggerExit2D(Collider2D other) {
		if (other.GetComponent<SpaceShipController>())
		{
			if (warningDisplay != null)
			{
				warningDisplay.SetActive(false);
			}
			takeDamage = false;
		}
	}

	private IEnumerator Countdown() {
		float duration = 3f;
		float normalizedTime = 0;
		while (normalizedTime <= 1f) {
			if (takeDamage == false) {
				yield break;
			}
			normalizedTime += Time.deltaTime / duration;
			yield return null;
		}

		//Restart coroutine
		spaceShip.TakeDamage(1);
		StartCoroutine(Countdown());
	}
}
