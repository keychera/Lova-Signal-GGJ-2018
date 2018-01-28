using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggActivator : MonoBehaviour {
	List<Egg> allEggs;
	CarnieHill hill;
	bool isActivated = false;

	void Start () {
		allEggs = new List<Egg>(GetComponentsInChildren<Egg>());
		hill = GetComponentInChildren<CarnieHill>();
	}
	
	void Update () {
		if (AllEggIsNotMovingDown()) {
			foreach (var Egg in allEggs)
			{
				Egg.eggActivated = true;
			}
			isActivated = true;
		}
		if (isActivated) {
			hill.Climb();
		}
	}

    private bool AllEggIsNotMovingDown()
    {
        bool allIsWell = true;
		foreach (var Egg in allEggs)
		{	
			allIsWell &= !Egg.IsMovingDown;
		}
		return allIsWell;
    }
}
