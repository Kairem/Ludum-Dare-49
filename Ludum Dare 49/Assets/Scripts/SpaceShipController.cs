using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SpaceShipController : MonoBehaviour {
	//########## PUBLIC SETTINGS #######################
	public float rotationDegreePerSec = 180f;
	public float force = 20.0f;

	//########## PARTICLE CONNECTIONS ##################
	public ParticleSystem emitter1;
	public ParticleSystem emitter2;

	//########## PREFAB CONNECTIONS ####################
	public GameObject explosionPrefab;

	//########## GUI CONNECTIONS #######################
	public GameObject deathPanel;
	public EditHearts uiHearts;
	public GameObject dayCounterPanel;

	//########## SOUNDS ################################
	public AudioClip harpoonSound;
	public AudioClip takeDamageSound;
	public MusicManager musicManager;

	//########## PRIVATE DECLARATIONS ##################
	bool isThrusting;
	float turnDirection;

	public int hearts = 3;
	bool isAlive = true;
	Rigidbody2D rb;

	//########## HARPOON STUFF ###########################
	enum HarpoonState {
		Ready,
		Out
	}
	HarpoonState hState = HarpoonState.Ready;
	bool isHarpoonCooldown = false;
	public GameObject tetherPrefab;
	GameObject tether;
	DistanceJoint2D tetherJoint;

	//########## UNITY FUNCTIONS #########################

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

	//########## OUR DEFINED FUNCTIONS ##################

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
		if (isAlive == true) {
			GetComponent<AudioSource>().volume = 0.8f;
			GetComponent<AudioSource>().PlayOneShot(takeDamageSound);
		}
		//Check if dead
		if (hearts <= 0 && isAlive == true) {
			isAlive = false;
			if (deathPanel != null) {
				musicManager.PlayDeathSoundtrack();
				int day = dayCounterPanel.GetComponent<ChangeText>().day;
				deathPanel.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().text = "You prolonged the sun for " + day + " months.";
				deathPanel.SetActive(true);
			}
			Die();
		}
	}

	public void Die() {
		print(gameObject.name + " was killed.");
		Instantiate(explosionPrefab, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}
