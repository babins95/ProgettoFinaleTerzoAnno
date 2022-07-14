using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateShadow : MonoBehaviour
{
    public float posX;
    public float posY;
    public float defaultY;

    SpriteRenderer renderer;
    public Sprite crateSprite;
    public Sprite mirrorCrateSprite;

    void Start()
    {
        posX = transform.localPosition.x;
        renderer = GetComponent<SpriteRenderer>();
    }

    public void TurnOnShadow(bool isMirror)
    {
        renderer.enabled = true;
        if(isMirror)
        {
            renderer.sprite = mirrorCrateSprite;
        }
        else
        {
            renderer.sprite = crateSprite;
        }
    }

    public void TurnOffShadow()
    {
        renderer.enabled = false;
    }
}
