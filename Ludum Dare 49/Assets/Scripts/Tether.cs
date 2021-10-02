using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tether : MonoBehaviour {
	public int maxLength = 20;
	public Rigidbody2D target;
	Rigidbody2D tetherPoint;

	void Start() {
		tetherPoint = GetComponent<Rigidbody2D>();
	}

	void Update() {

	}
}
