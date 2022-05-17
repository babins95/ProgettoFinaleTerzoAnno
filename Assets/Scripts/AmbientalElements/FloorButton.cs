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
                objectToChange.GetComponent<MovingPlatform>().enabled = switchForActivatingPlatform;
            }
            else if (buttonON == false)
            {
                objectToChange.GetComponent<MovingPlatform>().enabled = !switchForActivatingPlatform;
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
                objectToChange.GetComponent<RotatingPlatform>().enabled = switchForActivatingPlatform;
            }
            else if (buttonON == false)
            {

                objectToChange.GetComponent<RotatingPlatform>().enabled = !switchForActivatingPlatform;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (objectToChange.GetComponent<RotatingPlatform>())
        {
            objectToChange.GetComponent<RotatingPlatform>().IsConnectedWithButton = true;
        }
        else
        {
            objectToChange.GetComponent<MovingPlatform>().IsConnectedWithButton = true;
        }
    }
    //interruttore sempre attivo finchè c'è qualcosa sopra
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() || collision.GetComponent<Crate>())
        {
            buttonON = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() || collision.GetComponent<Crate>())
        {
            if (objectToChange.GetComponent<RotatingPlatform>())
            {
                objectToChange.GetComponent<RotatingPlatform>().IsConnectedWithButton = true;
            }
            else
            {
                objectToChange.GetComponent<MovingPlatform>().IsConnectedWithButton = true;
            }
            buttonON = false;
        }
    }
}
