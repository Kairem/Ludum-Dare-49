using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunDamageCollider : MonoBehaviour
{
	bool takeDamage;
	public SpaceShipController spaceShip;

	private void OnTriggerEnter2D(Collider2D other)
	{
		takeDamage = true;
		StartCoroutine(Countdown());
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		takeDamage = false;
	}

	private IEnumerator Countdown()
	{
		print("start co-routine to take damage");
		float duration = 3f;
		float normalizedTime = 0;
		while (normalizedTime <= 1f)
		{
			if (takeDamage == false)
			{
				yield break;
			}
			normalizedTime += Time.deltaTime / duration;
			yield return null;
		}
		print("Its been 3 seconds take damage!");

		//Restart coroutine
		spaceShip.TakeDamage(1);
		StartCoroutine(Countdown());
	}
}
