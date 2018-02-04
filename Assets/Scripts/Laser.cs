using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;


public class Laser : MonoBehaviour
{
    //LineRenderer line;
    public List<Vector3> lines;
    public enum Direction
    {
        Up,
        Right,
        Down,
        Left
    }

    public Direction initialDirection;
    public Vector2 laserDirection;
    public float laserLength = 10f;
    Vector2 startingPoint;
	public IInvoked invokedLater;

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
        lines = new List<Vector3>();
        lines.Add(transform.position);
    }

    void Update()
    {
        SyncDirection();
        //line.positionCount = 1;
        lines.RemoveRange(1,lines.Count - 1);
        bool keepReflecting = true;
        //line.SetPosition(0, transform.position);
        lines[0] = transform.position;
        startingPoint = transform.position;
        int vertexCounter = 0;
        int reflectionCount = 0;
        do
        {
            int layerMask = LayerMask.GetMask("World");
            RaycastHit2D hit = Physics2D.Raycast((Vector2)startingPoint + laserDirection * 0.5f, laserDirection, laserLength,layerMask);
            vertexCounter++;
            //line.positionCount += 1;
            lines.Add(transform.position);
            if (hit)
            {
                reflectionCount += 1;
                if (hit.collider.GetComponent<Wall>()) {
                    //line.SetPosition(vertexCounter, (Vector3)hit.point);
                    lines[vertexCounter] = (Vector3)hit.point;
                } else {
                    //line.SetPosition(vertexCounter, (Vector3)hit.collider.transform.position);
                    lines[vertexCounter] = (Vector3)hit.collider.transform.position;
                }
                Mirror mirror = hit.collider.GetComponent<Mirror>();
                if (mirror != null && reflectionCount < 100)
                {
                    //mirror.InvokeOnHit(laserDirection);
                    invokedLater = mirror.GetComponent<IInvoked>();
                    laserDirection = mirror.GetNextDirection(laserDirection);
                    if (laserDirection == Vector2.zero) {
                        keepReflecting = false;
                    } else {
                        startingPoint = mirror.transform.position;
                    }
                } else {
                    IInvoked hitByLaser = hit.collider.GetComponent<IInvoked>();
                    if(hitByLaser != null) {
                        //hitByLaser.InvokeOnHit(laserDirection);
                        invokedLater = hitByLaser;
                    } else {
                        invokedLater = null;
                    }
                    keepReflecting = false;
                }
            }
            else
            {
                if (vertexCounter == 1)
                {
                    //line.positionCount = 2;
                    if (lines.Count > 2) {
                        lines.RemoveRange(2,lines.Count - 2);
                    } else {
                        while(lines.Count != 2) {
                            lines.Add(transform.position);
                        }
                    }
                }
                //line.SetPosition(vertexCounter, (Vector3)startingPoint + (Vector3)laserDirection * laserLength);
                lines[vertexCounter] = (Vector3)startingPoint + (Vector3)laserDirection * laserLength;
                keepReflecting = false;
            }
        } while (keepReflecting);
    }
}
