using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallButton : MonoBehaviour
{
    private bool buttonON = false;
    //specifico se l'interruttore serve per fermare o attivare la rotazione
    public bool switchForActivatingPlatform;
    public GameObject objectToChange;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
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
    //interruttore sempre attivo finchè c'è qualcosa sopra
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() || collision.GetComponent<Crate>())
        {
            buttonON = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() || collision.GetComponent<Crate>())
        {
            buttonON = false;
        }
    }
}
