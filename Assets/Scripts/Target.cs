using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LaserHitDetector))]
public class Target : MonoBehaviour {
	LaserHitDetector detector;
	bool isHit;

	void Awake() {
		detector = GetComponent<LaserHitDetector>();
	}

	void OnEnable () {
		detector.OnHit += IsHit;
	}

	void OnDisable() {
		detector.OnHit -= IsHit;
	}

	void IsHit() {
		GetComponent<SpriteRenderer>().color = Color.black;
		isHit = true;
		GameController.Instance.SucceedGame();
	}

	void LateUpdate() {
		if (isHit) {
			isHit = false;
		} else {
			GetComponent<SpriteRenderer>().color = Color.white;
		}
	}
}
