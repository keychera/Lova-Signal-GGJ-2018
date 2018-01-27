using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LaserHitDetector))]
public class Bomb : MonoBehaviour
{
    LaserHitDetector detector;
	SpriteRenderer sprite;

    void Awake()
    {
        detector = GetComponent<LaserHitDetector>();
		sprite = GetComponent<SpriteRenderer>();
    }
    void OnEnable()
    {
        detector.OnHit += IsHit;
    }

    void OnDisable()
    {
        detector.OnHit -= IsHit;
    }

    void IsHit()
    {
		StartCoroutine(Explode());
        Destroy(gameObject, 0.2f);
    }

    private IEnumerator Explode()
    {
        for (int i = 0; i < 200; i++)
        {
			sprite.color = new Color(sprite.color.r,sprite.color.g,sprite.color.b,sprite.color.a - 0.005f);
            yield return new WaitForSeconds(0.01f);
        }
    }

    void OnDestroy()
    {
        GameController.Instance.lives--;
    }
}
