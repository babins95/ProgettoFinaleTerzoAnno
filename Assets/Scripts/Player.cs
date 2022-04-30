using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public SwitchCounter SwitchCounter;
    public Rigidbody2D rb;
    public Vector2 moveVector;
    public int speed = 6;

    Collider2D wallCollider;
    GameObject wallToClimb; 
    Collider2D passageCollider;
    GameObject doorToPass;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = moveVector * speed;
    }

    //se entri nel collider di un muro scalabile lo salvi nella variabile wallCollider
    //e ti salvi il riferimento al muro che hai di fronte
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<ClimbableWall>())
        {
            wallCollider = collision.GetComponent<Collider2D>();
            wallToClimb = collision.gameObject;
        }
        //idem con le porte
        if(collision.gameObject.GetComponent<NarrowPassage>())
        {
            passageCollider = collision.GetComponent<Collider2D>();
            doorToPass = collision.gameObject;
        }
    }

    //e lo cancelli quando esci
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ClimbableWall>())
        {
            wallCollider = null;
            wallToClimb = null;
        }

        if (collision.gameObject.GetComponent<NarrowPassage>())
        {
            passageCollider = null;
            doorToPass = null;
        }
    }

    void OnMove(InputValue moveValue)
    {
        moveVector = moveValue.Get<Vector2>();
    }

    void OnSwap()
    {
        GameManager.swap = !GameManager.swap;
        SwitchCounter.SwitchUsed();
    }

    void OnInteract()
    {
        Climb();

        PassThrough();
    }

    void Climb()
    {
        //se è nel collider di un muro scalabile fa partire l'animazione di scalata, al
        //termine della quale sposta il giocatore in cima al muro
        //poi cancella i riferimenti al muro per evitare bug
        if (wallCollider != null && GameManager.swap && !wallToClimb.GetComponent<ClimbableWall>().goBack)
        {
            float newPlayerY = wallToClimb.GetComponent<ClimbableWall>().newY;
            wallToClimb.GetComponent<ClimbableWall>().goBack = true;
            //parte l'animazione, quando finisce si passa a
            transform.position = new Vector2(transform.position.x, newPlayerY);

            wallCollider = null;
            wallToClimb = null;
        }
        //ho messo un booleano per controllare se hai già scalato quel muro
        //a seconda del valore ti sposta o sopra il muro o sotto
        else if(wallCollider != null && GameManager.swap && wallToClimb.GetComponent<ClimbableWall>().goBack)
        {
            float newPlayerY = wallToClimb.GetComponent<ClimbableWall>().backY;
            wallToClimb.GetComponent<ClimbableWall>().goBack = false;
            transform.position = new Vector2(transform.position.x, newPlayerY);

            wallCollider = null;
            wallToClimb = null;
        }
    }

    void PassThrough()
    {
        //stessa cosa con i passaggi stretti
        if (passageCollider != null && !GameManager.swap && !doorToPass.GetComponent<NarrowPassage>().goBack)
        {
            float newPlayerX;
            float newPlayerY;
            if (doorToPass.GetComponent<NarrowPassage>().newX != 0)
            {
                newPlayerX = doorToPass.GetComponent<NarrowPassage>().newX;
            }
            else
            {
                newPlayerX = transform.position.x;
            }

            if (doorToPass.GetComponent<NarrowPassage>().newY != 0)
            {
                newPlayerY = doorToPass.GetComponent<NarrowPassage>().newY;
            }
            else
            {
                newPlayerY = transform.position.y;
            }

            doorToPass.GetComponent<NarrowPassage>().goBack = true;
            transform.position = new Vector2(newPlayerX, newPlayerY);
            passageCollider = null;
            doorToPass = null;
        }
        else if(passageCollider != null && !GameManager.swap && doorToPass.GetComponent<NarrowPassage>().goBack)
        {
            float newPlayerX;
            float newPlayerY;
            if (doorToPass.GetComponent<NarrowPassage>().backX != 0)
            {
                newPlayerX = doorToPass.GetComponent<NarrowPassage>().backX;
            }
            else
            {
                newPlayerX = transform.position.x;
            }

            if (doorToPass.GetComponent<NarrowPassage>().backY != 0)
            {
                newPlayerY = doorToPass.GetComponent<NarrowPassage>().backY;
            }
            else
            {
                newPlayerY = transform.position.y;
            }

            doorToPass.GetComponent<NarrowPassage>().goBack = false;
            transform.position = new Vector2(newPlayerX, newPlayerY);
            passageCollider = null;
            doorToPass = null;
        }
    }

    void OnReset()
    {
        //ricarica la scena, messo solo per il debug, da togliere in futuro
        SceneManager.LoadScene("SampleScene");
    }

    void OnDeleteSave()
    {
        //cancella i file salvati, messo solo per i debug, da togliere in futuro
        PlayerPrefs.DeleteAll();
    }
}
