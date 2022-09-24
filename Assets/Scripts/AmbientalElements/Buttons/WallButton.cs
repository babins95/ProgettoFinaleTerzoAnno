using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallButton : MonoBehaviour
{
    private bool buttonON = false;
    //specifico se l'interruttore serve per fermare o attivare la rotazione
    public bool switchForActivatingPlatform;
    public GameObject objectToChange;
    public WallButton SwappedButton;
    public Sprite eyeClosed;
    private Sprite eyeOpen;
    private void Start()
    {
        eyeOpen = GetComponent<SpriteRenderer>().sprite;
    }
    void Update()
    {
        BlockRotating();
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
    //interruttore sempre attivo finchè c'è qualcosa sopra
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<LaserCollision>())
        {
            buttonON = !buttonON;
            GetComponent<SpriteRenderer>().sprite = eyeClosed;
            //con la piattaforma che si muove attivo semplicemente il movimento per poi disattivarlo nel MovingPlatform
            objectToChange.GetComponent<MovingPlatform>().MovePlatformToNextPos(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        GetComponent<SpriteRenderer>().sprite = eyeOpen;
        objectToChange.GetComponent<MovingPlatform>().MovePlatformToNextPos(false);
    }
}
