using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {
	LineRenderer line;

	void Start() {
		line = GetComponent<LineRenderer>();
		line.positionCount = 2;
		line.SetPosition(0,transform.position);
		line.SetPosition(1,transform.position);
	}

	void Update() {
		RaycastHit2D hit = Physics2D.Raycast(transform.position,transform.position - Input.mousePosition,50);
		line.SetPosition(1,hit.point);
		if(hit && hit.collider.GetComponent<Mirror>()) {
			Vector3 newDirection = Vector3.Reflect(Vector3.left, hit.normal);
			RaycastHit2D nextHit = Physics2D.Raycast(hit.point + 5* Vector2.right,newDirection,50);
			line.positionCount = 3;
			line.SetPosition(2,new Vector2(2,-10));
		} else {
			line.positionCount = 2;
		}
	}

	void TowardMouse() {
		Vector3 mouse_pos = Input.mousePosition;
		mouse_pos.z = 5.23f; //The distance between the camera and object
		Vector3 object_pos = Camera.main.WorldToScreenPoint(transform.position);
		mouse_pos.x = mouse_pos.x - object_pos.x;
		mouse_pos.y = mouse_pos.y - object_pos.y;
		float angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
	}
}
