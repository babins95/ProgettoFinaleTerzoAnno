using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    FixedJoint2D joint;
    Rigidbody2D rb;
    public bool pickedUp;

    bool onCratere;
    bool ignoreCollision;

    private void Start()
    {
        joint = GetComponent<FixedJoint2D>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponentInParent<Adult>() && !ignoreCollision)
        {
            collision.GetComponentInParent<Player>().interactableObject = gameObject;
        }

        if(collision.GetComponent<Cratere>())
        {
            onCratere = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<Adult>() && !pickedUp)
        {
            collision.GetComponentInParent<Player>().interactableObject = null;
        }

        if (collision.GetComponent<Cratere>())
        {
            onCratere = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.GetComponentInParent<Adult>() && !ignoreCollision)
        {
            collision.GetComponentInParent<Player>().interactableObject = gameObject;
        }
    }

    public void CrateInteraction(GameObject pickingUp)
    {
        if(!pickedUp)
        {
            if (!ignoreCollision)
            {
                PickUpCrate(pickingUp);
            }
        }
        else
        {
            PutDownCrate(pickingUp);
        }

    }

    //collego la cassa al player e tolgo i constraint al rigidbody, tranne il blocco alla rotazione
    private void PickUpCrate(GameObject pickingUp)
    {
        joint.enabled = true;
        joint.connectedBody = pickingUp.GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.None;
        pickedUp = true;
    }

    //stacco la cassa e riattivo i constraint al rigidbody
    private void PutDownCrate(GameObject puttingDown)
    {
        pickedUp = false;
        joint.enabled = false;
        joint.connectedBody = null;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        puttingDown.GetComponent<Player>().interactableObject = null;

        //se posi la cassa sopra un buco ci puoi camminare sopra e non la puoi più prendere
        if(onCratere)
        {
            ignoreCollision = true;
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }
}
