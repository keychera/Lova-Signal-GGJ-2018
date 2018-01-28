using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarnieHill : MonoBehaviour {
    internal void Climb()
    {
        transform.position = Vector3.MoveTowards(
			transform.position,
			new Vector2(0,-4.5f),
			0.01f
		);
    }
}
