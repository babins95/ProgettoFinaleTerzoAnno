using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProvaLaserPointer : MonoBehaviour
{
    public List<GameObject> lasersShooting;
    private void Start()
    {
        lasersShooting = new List<GameObject>();
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
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.GetComponent<ProvaShootLaser>().laserOn)
            {
                lasersShooting.Add(transform.GetChild(i).gameObject);
            }
        }

        UpdateLaser();
    }

    public void UpdateLaser()
    {
        foreach (GameObject laser in lasersShooting)
        {
            laser.GetComponent<ProvaShootLaser>().GoNextLaser();
        }
        lasersShooting = new List<GameObject>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ChildBullet>())
        {
            Rotate();
        }
    }
}
