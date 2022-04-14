using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhasingWall : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    BoxCollider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Player.swap)
        {
            spriteRenderer.enabled = false;
            collider.enabled = false;
        }
        else
        {
            spriteRenderer.enabled = true;
            collider.enabled = true;
        }
    }
}
