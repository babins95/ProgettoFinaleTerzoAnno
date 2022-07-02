using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProvaShootLaser : MonoBehaviour
{
    public GameObject Laser;
    ProvaLaserBeam laser;

    public ProvaShootLaser nextLaser;
    //[HideInInspector]
    //public bool turnOffLaser;
    public bool laserOn;
    private void Start()
    {
        if(laserOn == true)
        {
            CreateLaser();
        }
    }
    //per risparmiare sulle prestazioni si potrebbe spengere/accendere i laser a seconda
    //di quale livello sta facendo in quel momento il giocatore
    void Update()
    {
        //se c'è già un raggio lo distruggo prima di andare a creare un nuovo raggio
        //if (laser != null)
        //{
        //    Destroy(laser.laserObject);
        //}

        //if (laserOn)
        //{
        //    laser = Instantiate(Laser,transform.position,Quaternion.identity).GetComponent<ProvaLaserBeam>();
        //}
    }
    public void CreateLaser()
    {
        if (laser == null)
        {
            laser = Instantiate(Laser,GetComponentInParent<Transform>()).GetComponent<ProvaLaserBeam>();
        }
    }

    public void GoNextLaser()
    {
        laserOn = false;
        if(laser != null)
        {
            Destroy(laser.gameObject);
        }
        nextLaser.laserOn = true;
        nextLaser.CreateLaser();
    }
}
