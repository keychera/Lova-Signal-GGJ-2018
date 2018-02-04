using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class LaserHitDetector : MonoBehaviour, IInvoked
{
    public delegate void OnHitAction();
    public event OnHitAction OnHit;

    public void InvokeOnHit(Vector2 laserDirection)
    {
        if (OnHit != null)
        {
            OnHit();
        }
    }
}
