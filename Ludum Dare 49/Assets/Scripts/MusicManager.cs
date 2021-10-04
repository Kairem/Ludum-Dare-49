using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {
	public AudioClip deathSoundtrack;
	public AudioClip originalSoundtrack;
	public AudioClip battleSoundtrack;

	public void PlayOriginalSoundtrack() {
		GetComponent<AudioSource>().Stop();
		GetComponent<AudioSource>().volume = 0.1f;
		GetComponent<AudioSource>().loop = true;
		GetComponent<AudioSource>().clip = originalSoundtrack;
		GetComponent<AudioSource>().Play();
	}
	public void PlayBattleSoundtrack() {
		GetComponent<AudioSource>().Stop();
		GetComponent<AudioSource>().volume = 0.1f;
		GetComponent<AudioSource>().loop = true;
		GetComponent<AudioSource>().clip = battleSoundtrack;
		GetComponent<AudioSource>().Play();
	}
	public void PlayDeathSoundtrack() {
		GetComponent<AudioSource>().Stop();
		GetComponent<AudioSource>().volume = 0.1f;
		GetComponent<AudioSource>().loop = true;
		GetComponent<AudioSource>().clip = deathSoundtrack;
		GetComponent<AudioSource>().Play();
	}
}
