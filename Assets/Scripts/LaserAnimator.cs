using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(Laser))]
public class LaserAnimator : MonoBehaviour
{
    LineRenderer line;
    Laser laser;
    int numberOfCurrentPoints;
    int currentlyAnimated;
    public float distancePerFrame = 0.1f;

    void Awake()
    {
        line = GetComponent<LineRenderer>();
        laser = GetComponent<Laser>();
        numberOfCurrentPoints = 0;
        currentlyAnimated = 1;
    }

    void Update()
    {

        if (numberOfCurrentPoints == laser.lines.Count)
        {
            if (Vector3.Distance(line.GetPosition(currentlyAnimated), (laser.lines[currentlyAnimated])) > 0.1f)
            {
                line.SetPosition(currentlyAnimated, Vector3.Lerp(line.GetPosition(currentlyAnimated), laser.lines[currentlyAnimated], distancePerFrame));

            }
            else if (line.positionCount < laser.lines.Count)
            {
                line.positionCount++;
                currentlyAnimated++;
                line.SetPosition(currentlyAnimated, line.GetPosition(currentlyAnimated - 1));
            }
            else
            {
                IInvoked toInvoke = laser.invokedLater;
                if (toInvoke != null)
                {
                    toInvoke.InvokeOnHit(laser.laserDirection);
                }
            }
        }
        else
        {
            numberOfCurrentPoints = laser.lines.Count;
            line.positionCount = 2;
            line.SetPosition(0, transform.position);
            line.SetPosition(1, transform.position);
            currentlyAnimated = 1;
        }
        /*
		old, boring line rendering
		
		line.positionCount = laser.lines.Count;
		int i = 0;
		foreach (var laserPoint in laser.lines)
		{
			line.SetPosition(i,laserPoint);
			i++;
		}
		*/
    }
}
