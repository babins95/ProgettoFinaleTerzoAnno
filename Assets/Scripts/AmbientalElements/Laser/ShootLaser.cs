using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLaser : MonoBehaviour
{
    public GameObject Laser;
    public LaserBeam laser;

    public ShootLaser nextLaser;
    public bool laserOn;
    private void Start()
    {
        if (laserOn == true)
        {
            CreateLaser();
        }
    }
    public void CreateLaser()
    {
        if (laser != null)
        {
            Destroy(laser.gameObject);
        }
        else if (laser == null)
        {
            laser = Instantiate(Laser, GetComponentInParent<Transform>()).GetComponent<LaserBeam>();
        }
    }

    public void GoNextLaser()
    {
        if(laserOn == true)
        {
            laser = Instantiate(Laser, GetComponentInParent<Transform>()).GetComponent<LaserBeam>();
        }
    }
}
