using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLaser : MonoBehaviour
{
    public Material material;
    [SerializeField] float laserRange;
    LaserBeam laser;
    LaserBeam laser1;

    public bool laserRight;
    public bool laserLeft;
    public bool laserUp;
    public bool laserDown;

    //per risparmiare sulle prestazioni si potrebbe spengere/accendere i laser a seconda
    //di quale livello sta facendo in quel momento il giocatore
    void Update()
    {
        //se c'è già un raggio lo distruggo prima di andare a creare un nuovo raggio
        if (laser != null)
        {
            Destroy(laser.laserObject);
        }
        if (laser1 != null)
        {
            Destroy(laser1.laserObject);
        }

        if (GetComponentInParent<LaserPointer>().laserOn)
        {
            SpawnLaserBeams();
        }
    }

    void SpawnLaserBeams()
    {
        if(laserRight)
        {
            laser = new LaserBeam(gameObject.transform.position, gameObject.transform.right, material, laserRange);
        }
        if(laserLeft)
        {
            laser1 = new LaserBeam(gameObject.transform.position, -gameObject.transform.right, material, laserRange);
        }
        if(laserUp)
        {
            laser = new LaserBeam(gameObject.transform.position, gameObject.transform.up, material, laserRange);
        }
        if(laserDown)
        {
            laser1 = new LaserBeam(gameObject.transform.position, -gameObject.transform.up, material, laserRange);
        }
    }
}
