using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipController : MonoBehaviour {
	// Initial Variables
	public float rotationDegreePerSec = 180f;
	public float force = 20.0f;
	public Rigidbody2D rb;

	private bool isThrusting;
	private float turnDirection;

	/// Harpoon Variables ///
	enum HarpoonState {
		Ready,
		Out
	}
	HarpoonState hState = HarpoonState.Ready;
	bool isHarpoonCooldown = false;
	public GameObject tetherPrefab;
	GameObject tether;

	private void Awake() {
		rb = GetComponent<Rigidbody2D>();
		rb.drag = 1.5f;
	}

	// Update is called once per frame
	void Update() {
		ProcessInputs();
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
		tether = Instantiate(tetherPrefab, gameObject.transform);
		tether.transform.Find("Link").GetComponent<HingeJoint2D>().connectedBody = gameObject.GetComponent<Rigidbody2D>();
	}

	void retractHarpoon() {
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
			StartCoroutine(setCooldown(1f));
		} else if (hState == HarpoonState.Out) {
			print("Retracting");
			retractHarpoon();
			isHarpoonCooldown = true;
			StartCoroutine(setCooldown(1f));
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
}
