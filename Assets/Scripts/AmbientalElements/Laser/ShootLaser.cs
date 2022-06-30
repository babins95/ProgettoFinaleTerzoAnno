using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLaser : MonoBehaviour
{
    public Material material;
    [SerializeField] float laserRange;
    LaserBeam laser;

    public ShootLaser nextLaser;
    //[HideInInspector]
    //public bool turnOffLaser;
    public bool laserOn;

    //per risparmiare sulle prestazioni si potrebbe spengere/accendere i laser a seconda
    //di quale livello sta facendo in quel momento il giocatore
    void Update()
    {
        //se c'è già un raggio lo distruggo prima di andare a creare un nuovo raggio
        if (laser != null)
        {
            Destroy(laser.laserObject);
        }

        if (laserOn)
        {
            laser = new LaserBeam(gameObject.transform.position, gameObject.transform.right, material, laserRange);
        }
    }

    public void GoNextLaser()
    {
        laserOn = false;
        nextLaser.laserOn = true;
    }
}
