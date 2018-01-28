using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : Singleton<GameController> {
	public int lives = 3;
	public enum Status {
		Playing,
		Failed,
		Succeeded
	}

	public Status gameStatus;

	void OnEnable() {
		SceneManager.sceneLoaded += LevelStart;
	}

	void OnDisable() {
		SceneManager.sceneLoaded -= LevelStart;
	}

	void LevelStart(Scene scene, LoadSceneMode mode) {
		gameStatus = Status.Playing;
	}

	void Update() {
		if (lives < 0 || Input.GetKeyDown(KeyCode.L)) {
			gameStatus = Status.Failed;
		}
		if (Input.GetKeyDown(KeyCode.K)) {
			gameStatus = Status.Succeeded;
		}
	}

	public void SucceedGame() {
		gameStatus = Status.Succeeded;
	}

	public void FailGame() {
		gameStatus = Status.Failed;
	}
}