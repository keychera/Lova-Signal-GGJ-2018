using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStatusUI : MonoBehaviour {
	GameController game;
	Text text;
	public GameObject successBanner;
	public GameObject failBanner;

	void Awake() {
		game = GameController.Instance;
		text = GetComponent<Text>();
		text.text = "";
	}

	void OnEnable() {
		successBanner.SetActive(false);
		failBanner.SetActive(false);
	}

	void Update () {
		//text.text = "lives : " + game.lives + "\n" + "status : " + game.gameStatus.ToString();
		if (game.gameStatus == GameController.Status.Succeeded) {
			successBanner.SetActive(true);
		} else if (game.gameStatus == GameController.Status.Failed) {
			failBanner.SetActive(true);
		}
	}
}
