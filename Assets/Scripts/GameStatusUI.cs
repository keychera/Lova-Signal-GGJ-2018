using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStatusUI : MonoBehaviour {
	GameController game;
	Text text;

	void Awake() {
		game = GameController.Instance;
		text = GetComponent<Text>();
	}

	void Update () {
		text.text = "lives : " + game.lives + "\n" + "status : " + game.gameStatus.ToString();
	}
}
