using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LaserPointer : MonoBehaviour
{
    List<GameObject> lasersShooting;
    [HideInInspector]
    public List<GameObject> lasersToAdd;

    private void Start()
    {
        lasersShooting = new List<GameObject>();
        lasersToAdd = new List<GameObject>();
    }

    private void Rotate()
    {
        //animazione rotazione
        //a fine animazione metto un evento che chiama getlaseractive
        //per ora lo chiamo a mano
        GetLaserActive();
    }

    public void GetLaserActive()
    {    
        for(int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).gameObject.GetComponent<ShootLaser>().laserOn)
            {
                lasersToAdd.Add(transform.GetChild(i).gameObject);
            }
        }

        UpdateLaser();
    }

    public void UpdateLaser()
    {
        lasersShooting.AddRange(lasersToAdd);

        foreach(GameObject laser in lasersShooting)
        {
            laser.GetComponent<ShootLaser>().GoNextLaser();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ChildBullet>())
        {
            Rotate();
        }
    }
}