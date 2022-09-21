using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorButton : MonoBehaviour
{
    private bool buttonON = false;
    //specifico se l'interruttore serve per fermare o attivare la rotazione
    public bool switchForActivatingPlatform;
    public GameObject objectToChange;

    SpriteRenderer renderer;
    public Sprite on;
    Sprite off;
    AudioSource sound;

    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        sound = GetComponent<AudioSource>();
        off = renderer.sprite;
    }

    void Update()
    {
        BlockRotating();
        MovePlatform();
    }
    //Abilito o disabilito il movimento a seconda del tipo di interruttore
    private void MovePlatform()
    {
        if (objectToChange.GetComponent<MovingPlatform>())
        {
            if (buttonON == true)
            {
                objectToChange.GetComponent<MovingPlatform>().isConnectedWithFloorButton = switchForActivatingPlatform;
            }
            else if (buttonON == false)
            {
                objectToChange.GetComponent<MovingPlatform>().isConnectedWithFloorButton = !switchForActivatingPlatform;
            }
        }
    }

    //Abilito o disabilito la rotazione a seconda del tipo di interruttore
    private void BlockRotating()
    {
        if (objectToChange.GetComponent<RotatingPlatform>())
        {
            if (buttonON == true)
            {
                objectToChange.GetComponent<RotatingPlatform>().IsConnectedWithButton = switchForActivatingPlatform;
            }
            else if (buttonON == false)
            {

                objectToChange.GetComponent<RotatingPlatform>().IsConnectedWithButton = !switchForActivatingPlatform;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        sound.Play();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() || collision.GetComponent<Crate>())
        {
            buttonON = true;
            renderer.sprite = on;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() || collision.GetComponent<Crate>())
        {
            buttonON = false;
            renderer.sprite = off;
        }
    }
}
