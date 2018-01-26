using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorClickListener : MonoBehaviour {
	Mirror parentMirror;
	void Awake() {
		parentMirror = GetComponentInParent<Mirror>();
	}

	void OnMouseDown() {
		parentMirror.Rotate();
	}
}
