using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BulletSpawner : MonoBehaviour
{
    public bool bulletShotDown = false;
    public ChildBullet BulletPrefab;
    public float bulletFireRate = 2;
    private float timer;
    public float posX;
    public float posY;
    public Vector2 bulletDirection;
    Animator animator;

    void Start()
    {
        bulletDirection = Vector2.down;
        posX = transform.localPosition.x;
        posY = transform.localPosition.y;
        timer = bulletFireRate;
        animator = GetComponentInParent<Animator>();
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
        if (!GameManager.swap)
        {
            if (Player.canShoot)
            {
                if (timer >= bulletFireRate)
                {
                    timer = 0;

                    animator.SetBool("shooting", true);
                }
            }
        }
    }

    public void ActualShoot()
    {
        if (bulletShotDown == true)
        {
            BulletPrefab.GetComponent<SpriteRenderer>().sortingOrder = 2;
        }
        else
        {
            BulletPrefab.GetComponent<SpriteRenderer>().sortingOrder = 0;
        }
        BulletPrefab.bulletAngle = bulletDirection;
        ChildBullet newBullet = Instantiate(BulletPrefab);
        newBullet.transform.position = this.transform.position;
    }
}
