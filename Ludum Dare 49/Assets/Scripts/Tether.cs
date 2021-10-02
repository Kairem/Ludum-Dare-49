using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tether : MonoBehaviour {
	public Transform anchor1;
	public Transform anchor2;
	Vector3 anchor1Pos, anchor2Pos;

	public float length = 10;
	public int numSegments = 35;

	private LineRenderer lineRenderer;
	private List<RopeSegment> ropeSegments = new List<RopeSegment>();
	float ropeSegLen;
	float lineWidth = 0.1f;

	void Start() {
		anchor1Pos = anchor1.position;
		anchor2Pos = anchor2.position;

		ropeSegLen = length / numSegments;

		this.lineRenderer = this.GetComponent<LineRenderer>();
		Vector3 ropeStartPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		for (int i = 0; i < numSegments; i++) {
			this.ropeSegments.Add(new RopeSegment(ropeStartPoint));
			ropeStartPoint.y -= ropeSegLen;
		}
	}

	void Update() {
		DrawRope();
	}

	void FixedUpdate() {
		Simulate();
	}

	void Simulate() {
		anchor1Pos = anchor1.position;
		anchor2Pos = anchor2.position;

		Vector2 forceGravity = new Vector2(0f, 0f);

		for (int i = 0; i < this.numSegments; i++) {
			RopeSegment firstSegment = this.ropeSegments[i];
			Vector2 velocity = firstSegment.posNow - firstSegment.posOld;
			firstSegment.posOld = firstSegment.posNow;
			firstSegment.posNow += velocity;
			firstSegment.posNow += forceGravity * Time.fixedDeltaTime;
			this.ropeSegments[i] = firstSegment;
		}

		// Add constraints
		for (int i = 0; i < 200; i++) {
			this.ApplyConstraints();
		}
	}

	void ApplyConstraints() {
		RopeSegment firstSegment = this.ropeSegments[0];
		firstSegment.posNow = anchor1Pos;
		this.ropeSegments[0] = firstSegment;

		RopeSegment endSegment = this.ropeSegments[numSegments - 1];
		endSegment.posNow = anchor2Pos;
		this.ropeSegments[numSegments - 1] = endSegment;

		for (int i = 0; i < this.numSegments - 1; i++) {
			RopeSegment firstSeg = this.ropeSegments[i];
			RopeSegment secondSeg = this.ropeSegments[i + 1];

			float dist = (firstSeg.posNow - secondSeg.posNow).magnitude;
			float error = Mathf.Abs(dist - this.ropeSegLen);
			Vector2 changeDir = Vector2.zero;

			if (dist > ropeSegLen) {
				changeDir = (firstSeg.posNow - secondSeg.posNow).normalized;
			} else if (dist < ropeSegLen) {
				changeDir = (secondSeg.posNow - firstSeg.posNow).normalized;
			}

			Vector2 changeAmount = changeDir * error;
			if (i != 0) {
				firstSeg.posNow -= changeAmount * .5f;
				this.ropeSegments[i] = firstSeg;
				secondSeg.posNow += changeAmount * .5f;
				this.ropeSegments[i + 1] = secondSeg;
			} else {
				secondSeg.posNow += changeAmount;
				this.ropeSegments[i + 1] = secondSeg;
			}
		}
	}

	private void DrawRope() {
		float lineWidth = this.lineWidth;
		lineRenderer.startWidth = lineWidth;
		lineRenderer.endWidth = lineWidth;

		Vector3[] ropePositions = new Vector3[this.numSegments];
		for (int i = 0; i < this.numSegments; i++) {
			ropePositions[i] = this.ropeSegments[i].posNow;
		}

		lineRenderer.positionCount = ropePositions.Length;
		lineRenderer.SetPositions(ropePositions);
	}

	public struct RopeSegment {
		public Vector2 posNow;
		public Vector2 posOld;

		public RopeSegment(Vector2 pos) {
			this.posNow = pos;
			this.posOld = pos;
		}
	}
}
