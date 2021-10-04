using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipController : MonoBehaviour {
	// Initial Variables
	public float rotationDegreePerSec = 180f;
	public float force = 20.0f;
	public ParticleSystem emitter1;
	public ParticleSystem emitter2;
	public GameObject deathPanel;
	Rigidbody2D rb;
	public EditHearts uiHearts;
	public AudioClip harpoonSound;
	public AudioClip takeDamageSound;
	public MusicManager musicManager;

	private bool isThrusting;
	private float turnDirection;

	public int hearts = 3;
	bool isAlive = true;

	/// Harpoon Variables ///
	enum HarpoonState {
		Ready,
		Out
	}
	HarpoonState hState = HarpoonState.Ready;
	bool isHarpoonCooldown = false;
	public GameObject tetherPrefab;
	GameObject tether;
	DistanceJoint2D tetherJoint;

	private void Awake() {
		rb = GetComponent<Rigidbody2D>();
		rb.drag = 1.5f;

		tetherJoint = gameObject.GetComponent<DistanceJoint2D>();
	}

    private void Start() {
		musicManager.PlayOriginalSoundtrack();
	}
	// Update is called once per frame
	void Update() {
		ProcessInputs();

		if (isThrusting) {
			emitter1.Play();
			emitter2.Play();
		} else {
			emitter1.Stop();
			emitter2.Stop();
		}
	}

	// physics calculations
	private void FixedUpdate() {
		Move();
	}

	IEnumerator setCooldown(float seconds) {
		yield return new WaitForSeconds(seconds);
		if (hState == HarpoonState.Ready) {
			hState = HarpoonState.Out;
		} else {
			hState = HarpoonState.Ready;
		}
		isHarpoonCooldown = false;
	}

	void fireHarpoon() {
		GetComponent<AudioSource>().volume = 0.1f;
		GetComponent<AudioSource>().PlayOneShot(harpoonSound);
		tether = Instantiate(tetherPrefab, gameObject.transform);
		//tether.transform.Find("Link").GetComponent<HingeJoint2D>().connectedBody = gameObject.GetComponent<Rigidbody2D>();
		Tether tetherComponent = tether.GetComponent<Tether>();
		tetherComponent.anchor1 = gameObject.transform;
		tetherComponent.anchor2 = tether.transform;
		tetherComponent.enabled = true;
	}

	void retractHarpoon() {
		tetherJoint.enabled = false;
		Destroy(tether);
	}

	void triggerHarpoon() {
		if (isHarpoonCooldown) {
			print("On cooldown...");
			return;
		}
		if (hState == HarpoonState.Ready) {
			print("FIREEEEEE");
			fireHarpoon();
			isHarpoonCooldown = true;
			StartCoroutine(setCooldown(.0f));
		} else if (hState == HarpoonState.Out) {
			print("Retracting");
			retractHarpoon();
			isHarpoonCooldown = true;
			StartCoroutine(setCooldown(.0f));
		}
	}

	void ProcessInputs() {
		isThrusting = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

		turnDirection = -Input.GetAxisRaw("Horizontal");

		if (Input.GetKeyDown(KeyCode.Space)) {
			triggerHarpoon();
		}
	}

	void Move() {
		if (isThrusting) {
			rb.AddForce(force * gameObject.transform.up);
		}

		if (turnDirection != 0.0f) {
			rb.angularVelocity = turnDirection * rotationDegreePerSec;
		} else {
			rb.angularVelocity = 0;
		}
	}

	public void TakeDamage(int damage) {
		hearts -= damage;
		//Update UI hearts
		uiHearts.UpdateGraphic(hearts);
		//Play hit sound
		if (isAlive == true)
        {
			GetComponent<AudioSource>().volume = 0.8f;
			GetComponent<AudioSource>().PlayOneShot(takeDamageSound);
		}
		//Check if dead
		if (hearts <= 0 && isAlive == true) {
			isAlive = false;
			if (deathPanel != null) {
				musicManager.PlayDeathSoundtrack();
				deathPanel.SetActive(true);
			}
		}
	}
}
