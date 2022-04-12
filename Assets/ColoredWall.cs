using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColoredWall : MonoBehaviour
{
    SpriteRenderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Player.swap)
        {
            renderer.color = Color.red;
        }
        else
        {
            renderer.color = Color.green;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Player.swap)
        {
            collision.gameObject.SetActive(false);
        }
    }
}
