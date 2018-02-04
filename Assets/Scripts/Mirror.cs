using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour, IInvoked
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
        Through,
        Multi
    }
    public Type type;
    private bool isHit;
    private Vector2 laserDirection;
    public GameObject laserRenderer;
    private List<GameObject> generatedLasers;
    //uuhhh
    Vector2 multiLaserEnterWhatTheHeck;

    void Awake() {
        generatedLasers = new List<GameObject>();
    }


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
        if (type != Type.Static || type != Type.Multi)
        {
            SoundEffectManager.Instance.PlayRotate();
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
        } else {
            SoundEffectManager.Instance.PlayCantAct();
        }
    }

    internal Vector2 GetNextDirection(Vector2 laserDirection)
    {
        if (type == Type.Multi)
        {
            multiLaserEnterWhatTheHeck = laserDirection;
            return Vector2.zero;
        }
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
        if (type == Type.Through)
        {
            return laserDirection;
        }
        return Vector2.zero;
    }

    public void InvokeOnHit(Vector2 incomingLaserDirection)
    {
        isHit = true;
        laserDirection = incomingLaserDirection;
    }

    void LateUpdate() {
		if (isHit) {
			isHit = false;
            if (type == Type.Multi && generatedLasers.Count == 0) {
                GenerateMultiMirror(laserDirection);
            }
		} else {
            if (type == Type.Multi && generatedLasers.Count != 0) {
                DestroyLasers();
            }
			GetComponent<SpriteRenderer>().color = Color.white;
		}
	}

    private void DestroyLasers()
    {
        foreach (var Laser in generatedLasers)
        {
            Destroy(Laser,0.01f);
        }
        generatedLasers.Clear();
    }

    private void GenerateMultiMirror(Vector2 laserDirection)
    {
        if (multiLaserEnterWhatTheHeck != Vector2.up) {
            GameObject newRenderer = Instantiate(laserRenderer, transform.position,Quaternion.identity);
            newRenderer.GetComponent<Laser>().initialDirection = Laser.Direction.Down;
            generatedLasers.Add(newRenderer);
        }
        if (multiLaserEnterWhatTheHeck != Vector2.down) {
            GameObject newRenderer = Instantiate(laserRenderer, transform.position,Quaternion.identity);
            newRenderer.GetComponent<Laser>().initialDirection = Laser.Direction.Up;
            generatedLasers.Add(newRenderer);
        }
        if (multiLaserEnterWhatTheHeck != Vector2.right) {
            GameObject newRenderer = Instantiate(laserRenderer, transform.position,Quaternion.identity);
            newRenderer.GetComponent<Laser>().initialDirection = Laser.Direction.Left;
            generatedLasers.Add(newRenderer);
            
        }
        if (multiLaserEnterWhatTheHeck != Vector2.left) {
            GameObject newRenderer = Instantiate(laserRenderer, transform.position,Quaternion.identity);
            newRenderer.GetComponent<Laser>().initialDirection = Laser.Direction.Right;
            generatedLasers.Add(newRenderer);
        }
    }
}
