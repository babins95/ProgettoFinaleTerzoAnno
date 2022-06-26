using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointer : MonoBehaviour
{
    [HideInInspector]
    public bool laserOn;

    float startAngle;
    float currentAngle;
    float rotationSpeed = 100f;
    float angleToReach;
    float rotationAngle = 90;
    bool rotating;

    // Start is called before the first frame update
    void Start()
    {
        laserOn = true;
        startAngle = transform.eulerAngles.z;
        currentAngle = startAngle;
        angleToReach = currentAngle + rotationAngle;
    }

    void Update()
    {
        if (rotating)
        {
            if (currentAngle >= angleToReach)
            {
                if (currentAngle >= 360)
                {
                    currentAngle = startAngle;
                    angleToReach = currentAngle;
                }
                rotating = false;
                angleToReach = currentAngle + rotationAngle;
            }

            Rotate();
        }
        else
        {
            laserOn = true;
        }
    }

    private void Rotate()
    {
        transform.rotation = Quaternion.Euler(0, 0, currentAngle);
        currentAngle += rotationSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ChildBullet>())
        {
            laserOn = false;
            rotating = true;
        }
    }
}