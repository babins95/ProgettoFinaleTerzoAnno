using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLaser : MonoBehaviour
{
    public Material material;
    [SerializeField] float laserRange;
    LaserBeam laser;

    //per risparmiare sulle prestazioni si potrebbe spengere/accendere i laser a seconda
    //di quale livello sta facendo in quel momento il giocatore
    void Update()
    {
        //se c'è g'à un raggio lo distruggo prima di andare a creare un nuovo raggio
        if (laser != null)
        {
            Destroy(laser.laserObject);
        }
        laser = new LaserBeam(gameObject.transform.position, gameObject.transform.right, material, laserRange);
    }
}
