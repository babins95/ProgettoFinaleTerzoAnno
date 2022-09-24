using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCheck : MonoBehaviour
{
    Player player;
    public float posX;
    public float posY;

    void Start()
    {
        posX = transform.localPosition.x;
        player = GetComponentInParent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.GetComponent<FloorButton>() && !collision.GetComponent<LaserBeam>())
        {
            player.obstacleAhead = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.GetComponent<FloorButton>() && !collision.GetComponent<LaserBeam>())
        {
            player.obstacleAhead = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player.obstacleAhead = false;
    }
}
