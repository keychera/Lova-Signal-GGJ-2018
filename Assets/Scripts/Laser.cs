﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Laser : MonoBehaviour
{
    LineRenderer line;
    public enum Direction
    {
        Up,
        Right,
        Down,
        Left
    }

    public Direction initialDirection;
    Vector2 laserDirection;
    public float laserLength = 10f;
    Vector2 startingPoint;

    void OnValidate()
    {
        SyncDirection();
    }

    private void SyncDirection()
    {
        switch (initialDirection)
        {
            case Direction.Up:
                laserDirection = Vector2.up;
                break;
            case Direction.Right:
                laserDirection = Vector2.right;
                break;
            case Direction.Down:
                laserDirection = Vector2.down;
                break;
            case Direction.Left:
                laserDirection = Vector2.left;
                break;
        }
    }

    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    void Update()
    {
        SyncDirection();
        line.positionCount = 1;
        bool keepReflecting = true;
        line.SetPosition(0, transform.position);
        startingPoint = transform.position;
        int vertexCounter = 0;
        do
        {
            int layerMask = LayerMask.GetMask("World");
            RaycastHit2D hit = Physics2D.Raycast((Vector2)startingPoint + laserDirection * 1, laserDirection, laserLength,layerMask);
            vertexCounter++;
            line.positionCount += 1;
            if (hit)
            {
                line.SetPosition(vertexCounter, hit.point);
                Mirror mirror = hit.collider.GetComponent<Mirror>();
                if (mirror != null)
                {
                    laserDirection = GetNextDirection(mirror.direction);
                    if (laserDirection == Vector2.zero) {
                        keepReflecting = false;
                    } else {
                        startingPoint = hit.point;
                    }
                }
            }
            else
            {
                if (vertexCounter == 1)
                {
                    line.positionCount = 2;
                }
                line.SetPosition(vertexCounter, (Vector2)startingPoint + laserDirection * laserLength);
                keepReflecting = false;
            }
        } while (keepReflecting);



        /* 
        if (hit)
        {
            RaycastHit2D currentHit = hit;
            while (keepReflecting)
            {
                line.SetPosition(line.positionCount - 1, currentHit.point);
                Mirror mirror = currentHit.collider.GetComponent<Mirror>();
                if (mirror)
                {
                    Vector2 nextDirection = GetNextDirection(mirror.direction);
                    laserDirection = nextDirection;
                    RaycastHit2D nextHit = Physics2D.Raycast((Vector2)currentHit.point + nextDirection * 1, nextDirection, laserLength);
                    if (nextHit)
                    {
						if (Vector2.Distance(nextHit.point,currentHit.point) > 0.01f) {
							currentHit = nextHit;
							line.positionCount += 1;
						} else {
							keepReflecting = false;
						}
                    }
                    else
                    {
                        keepReflecting = false;
                    }
                }
                else
                {
                    keepReflecting = false;
                }
            }
        }
        else
        {
            line.SetPosition(1, (Vector2)transform.position + laserDirection * laserLength);
            line.positionCount = 2;
        }*/
    }

    Vector2 GetNextDirection(Mirror.Direction direction)
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
        return Vector2.zero;
    }
}
