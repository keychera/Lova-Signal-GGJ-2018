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
}
