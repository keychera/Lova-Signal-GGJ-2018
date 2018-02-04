using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInvoked
{
	void InvokeOnHit(Vector2 incomingLaserDirection);
}