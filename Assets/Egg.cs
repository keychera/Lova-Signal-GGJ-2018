using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LaserHitDetector))]
public class Egg : MonoBehaviour
{
    LaserHitDetector detector;
    private bool isHit;
    public bool IsMovingDown = true;
    public bool eggActivated = false;
    Vector3 originalLocation;

    void Awake()
    {
        detector = GetComponent<LaserHitDetector>();
        originalLocation = transform.position;
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
        isHit = true;
    }

    void LateUpdate()
    {
        if (isHit)
        {
            isHit = false;
            if (transform.position.y > -6 && IsMovingDown)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(
                    originalLocation.x,
                    originalLocation.y - 6,
                    originalLocation.z
                ), 0.01f);
            }
            else if (transform.position.y <= -6 && IsMovingDown)
            {
                if (!eggActivated)
                {
                    IsMovingDown = false;
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(
						originalLocation.x,
						originalLocation.y,
						originalLocation.z
                    ), 0.01f);
                }
            }
        }
        else
        {
            IsMovingDown = true;
			eggActivated = false;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(
                    originalLocation.x,
                    originalLocation.y,
                    originalLocation.z
            ), 0.01f);
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}
