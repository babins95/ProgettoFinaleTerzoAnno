using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    FixedJoint2D joint;
    Rigidbody2D rb;
    BoxCollider2D coll;
    public bool pickedUp;

    bool ignoreCollision;

    Vector2 eyePos;
    public float range = 0.5f;

    private void Start()
    {
        joint = GetComponent<FixedJoint2D>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<Adult>())
        {
            if (collision.GetComponent<PlayerEye>() && !ignoreCollision && !collision.GetComponentInParent<Adult>().hasCrate)
            {
                collision.GetComponentInParent<Player>().interactableObject = gameObject;
            }
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<Adult>())
        {
            if (collision.GetComponent<PlayerEye>() && !pickedUp && !ignoreCollision && !collision.GetComponentInParent<Adult>().hasCrate)
            {
                collision.GetComponentInParent<Player>().interactableObject = null;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.GetComponentInParent<Adult>())
        {
            if (collision.GetComponent<PlayerEye>() && !ignoreCollision && !collision.GetComponentInParent<Adult>().hasCrate)
            {
                collision.GetComponentInParent<Player>().interactableObject = gameObject;
            }        }
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
        if (!pickingUp.GetComponent<Adult>().hasCrate)
        {
            joint.enabled = true;
            joint.connectedBody = pickingUp.GetComponent<Rigidbody2D>();
            rb.constraints = RigidbodyConstraints2D.None;
            pickedUp = true;
            transform.position = new Vector2(pickingUp.transform.position.x, pickingUp.transform.position.y + 1);
            coll.enabled = false;
            pickingUp.GetComponent<Adult>().hasCrate = true;
        }
        else
        {
            //suono del "non puoi"
            Debug.Log("NO!");
        }
    }

    //stacco la cassa e riattivo i constraint al rigidbody
    private void PutDownCrate(GameObject puttingDown)
    {
        coll.enabled = true;
        pickedUp = false;
        joint.enabled = false;
        joint.connectedBody = null;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        puttingDown.GetComponent<Player>().interactableObject = null;
        eyePos = puttingDown.GetComponentInChildren<PlayerEye>().gameObject.transform.position;
        puttingDown.GetComponent<Adult>().hasCrate = false;

        switch (puttingDown.GetComponent<Player>().eyePosCounter)
        {
            case 1:
                transform.position = new Vector2(eyePos.x, eyePos.y + range);
                break;

            case 2:
                transform.position = new Vector2(eyePos.x, eyePos.y - range);
                break;

            case 3:
                transform.position = new Vector2(eyePos.x + range, eyePos.y);
                break;

            case 4:
                transform.position = new Vector2(eyePos.x - range, eyePos.y);
                break;
        }

        //se posi la cassa sopra un buco ci puoi camminare sopra e non la puoi più prendere
        if(puttingDown.GetComponentInChildren<PlayerEye>().onHole && !puttingDown.GetComponentInChildren<PlayerEye>().holeColliding.GetComponent<Hole>().filled)
        {
            puttingDown.GetComponentInChildren<PlayerEye>().holeColliding.GetComponent<Hole>().FillHole();
            gameObject.transform.position = puttingDown.GetComponentInChildren<PlayerEye>().holeColliding.GetComponent<Renderer>().bounds.center;
            ignoreCollision = true;
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }
}
