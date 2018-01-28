using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickToStart : MonoBehaviour {
	public Animator title, click, bg;

	void Start () {
		
	}
	
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			title.SetBool("isTheGameStarted",true);
			click.SetBool("isTheGameStarted",true);
			bg.SetBool("isTheGameStarted",true);
		}	
	}
}
