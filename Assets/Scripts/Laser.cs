using System.Collections;
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
                if (hit.collider.GetComponent<Wall>()) {
                    line.SetPosition(vertexCounter, (Vector3)hit.point);
                } else {
                    line.SetPosition(vertexCounter, (Vector3)hit.collider.transform.position);
                }
                Mirror mirror = hit.collider.GetComponent<Mirror>();
                if (mirror != null)
                {
                    laserDirection = mirror.GetNextDirection(laserDirection);
                    if (laserDirection == Vector2.zero) {
                        keepReflecting = false;
                    } else {
                        startingPoint = mirror.transform.position;
                    }
                } else {
                    LaserHitDetector hitByLaser = hit.collider.GetComponent<LaserHitDetector>();
                    if(hitByLaser != null) {
                        hitByLaser.InvokeOnHit();
                    }
                    keepReflecting = false;
                }
            }
            else
            {
                if (vertexCounter == 1)
                {
                    line.positionCount = 2;
                }
                line.SetPosition(vertexCounter, (Vector3)startingPoint + (Vector3)laserDirection * laserLength);
                keepReflecting = false;
            }
        } while (keepReflecting);
    }
}
