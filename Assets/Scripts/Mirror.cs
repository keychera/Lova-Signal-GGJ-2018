using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour {
    public enum Direction {
        first,
        second,
        third,
        fourth
    }

    public Direction direction;

    void OnValidate() {
        SyncDirection();
    }

    private void SyncDirection()
    {
        switch(direction) {
            case Direction.first:
                transform.rotation = Quaternion.Euler(0,0,45f);
                break;
            case Direction.second:
                transform.rotation = Quaternion.Euler(0,0,135f);
                break;
            case Direction.third:
                transform.rotation = Quaternion.Euler(0,0,-135f);
                break;
            case Direction.fourth:
                transform.rotation = Quaternion.Euler(0,0,-45f);
                break;
        }
    }

    public void Rotate() {
        switch(direction) {
            case Direction.first:
                direction = Direction.second;
                break;
            case Direction.second:
                direction = Direction.third;
                break;
            case Direction.third:
                direction = Direction.fourth;
                break;
            case Direction.fourth:
                direction = Direction.first;
                break;
        }
        SyncDirection();
    }
}
