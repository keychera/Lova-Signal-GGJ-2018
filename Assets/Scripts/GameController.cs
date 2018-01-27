using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController> {
	public int lives = 3;
	public enum Status {
		Playing,
		Failed,
		Succeeded
	}

	public Status gameStatus;

	void Awake() {
		gameStatus = Status.Playing;
	}

	void Update() {
		if (lives < 0) {
			gameStatus = Status.Failed;
		}
	}

	public void SucceedGame() {
		gameStatus = Status.Succeeded;
	}
}