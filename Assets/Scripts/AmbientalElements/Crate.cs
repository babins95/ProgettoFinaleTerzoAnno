using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    FixedJoint2D joint;
    Rigidbody2D rb;
    public bool pickedUp;

    bool onHole;
    bool ignoreCollision;

    GameObject holeColliding;

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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Hole>())
        {
            onHole = true;
            holeColliding = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<Adult>() && !pickedUp && !ignoreCollision)
        {
            collision.GetComponentInParent<Player>().interactableObject = null;
        }

        if (collision.GetComponent<Hole>())
        {
            onHole = false;
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
        if(onHole && !holeColliding.GetComponent<Hole>().filled)
        {
            holeColliding.GetComponent<Hole>().filled = true;
            gameObject.transform.position = holeColliding.GetComponent<Renderer>().bounds.center;
            ignoreCollision = true;
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }
}
