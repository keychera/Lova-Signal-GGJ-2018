using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWall : MonoBehaviour {

    public Transform wall1;
    public Transform wall2;

    // Use this for initialization
    void Start () {

        for (int y = -4; y < 5; y++) {
            for (int x = -4; x < 5; x++) {
                float randVal = Random.Range(0f, 1f);
                if (randVal < 0.5f)
                    Instantiate(wall1, new Vector3(x,y,0), Quaternion.identity);
                else
                    Instantiate(wall2, new Vector3(x, y, 0), Quaternion.identity);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
