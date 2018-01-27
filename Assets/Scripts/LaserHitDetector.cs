using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class LaserHitDetector : MonoBehaviour
{
    public delegate void OnHitAction();
    public event OnHitAction OnHit;

    internal void InvokeOnHit()
    {
        if (OnHit != null)
        {
            OnHit();
        }
    }
}
