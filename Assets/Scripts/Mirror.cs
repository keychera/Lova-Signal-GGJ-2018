using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    public enum Direction
    {
        first,
        second,
        third,
        fourth
    }
    public Direction direction;
    public enum Type
    {
        Rotatable,
        Static,
        Through
    }
    public Type type;

    void OnValidate()
    {
        SyncDirection();
    }

    private void SyncDirection()
    {
        switch (direction)
        {
            case Direction.first:
                transform.rotation = Quaternion.Euler(0, 0, 45f);
                break;
            case Direction.second:
                transform.rotation = Quaternion.Euler(0, 0, 135f);
                break;
            case Direction.third:
                transform.rotation = Quaternion.Euler(0, 0, -135f);
                break;
            case Direction.fourth:
                transform.rotation = Quaternion.Euler(0, 0, -45f);
                break;
        }
    }

    public void Rotate()
    {
        if (type != Type.Static)
        {
            switch (direction)
            {
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

    internal Vector2 GetNextDirection(Vector2 laserDirection)
    {
        switch (direction)
        {
            case Mirror.Direction.first:
                if (laserDirection == Vector2.left)
                {
                    return Vector2.up;
                }
                else if (laserDirection == Vector2.down)
                {
                    return Vector2.right;
                }
                break;
            case Mirror.Direction.second:
                if (laserDirection == Vector2.right)
                {
                    return Vector2.up;
                }
                else if (laserDirection == Vector2.down)
                {
                    return Vector2.left;
                }
                break;
            case Mirror.Direction.third:
                if (laserDirection == Vector2.right)
                {
                    return Vector2.down;
                }
                else if (laserDirection == Vector2.up)
                {
                    return Vector2.left;
                }
                break;
            case Mirror.Direction.fourth:
                if (laserDirection == Vector2.left)
                {
                    return Vector2.down;
                }
                else if (laserDirection == Vector2.up)
                {
                    return Vector2.right;
                }
                break;
        }
        if (type == Type.Through) {
            return laserDirection;    
        }
        return Vector2.zero;
    }
}
