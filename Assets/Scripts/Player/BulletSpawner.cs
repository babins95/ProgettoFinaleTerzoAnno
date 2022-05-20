using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BulletSpawner : MonoBehaviour
{
    public ChildBullet BulletPrefab;
    public float bulletFireRate = 2;
    private float timer;
    void Start()
    {
        timer = bulletFireRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < bulletFireRate)
        {
            timer += Time.deltaTime;
        }
    }
    void OnShoot()
    {
        if (timer >= bulletFireRate)
        {
            BulletPrefab.bulletAngle = Vector2.right;
            ChildBullet newBullet = Instantiate(BulletPrefab);
            newBullet.transform.position = this.transform.position;
            timer = 0;
        }
    }
}
