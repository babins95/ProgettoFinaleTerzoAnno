using System.Collections.Generic;
using UnityEngine;

public class LaserPointer : MonoBehaviour
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
            if (transform.GetChild(i).gameObject.GetComponent<ShootLaser>().laserOn)
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
            laser.GetComponent<ShootLaser>().laserOn = false;
        }
        foreach (GameObject laser in lasersShooting)
        {
            laser.GetComponent<ShootLaser>().nextLaser.laserOn = true;
        }
        foreach (GameObject laser in lasersShooting)
        {
            if(laser.GetComponent<ShootLaser>().laserOn == true)
            {
                laser.GetComponent<ShootLaser>().nextLaser.laser = Instantiate(laser.GetComponent<ShootLaser>().nextLaser.Laser, laser.GetComponent<ShootLaser>().nextLaser.GetComponentInParent<Transform>()).GetComponent<LaserBeam>();
            }
        }
        lasersShooting = new List<GameObject>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ChildBullet>())
        {
            GetLaserActive();
        }
    }
}
