using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWall : MonoBehaviour {
    public List<Sprite> sprites;

    // Use this for initialization
    void Start () {
        
        SpriteRenderer render;
        render = GetComponent<SpriteRenderer>();
        render.sprite = sprites.ToArray()[Random.Range(0,sprites.Count)];
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
