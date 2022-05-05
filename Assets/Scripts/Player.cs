using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public SwitchCounter SwitchCounter;
    public Rigidbody2D rb;
    [HideInInspector]
    public Vector2 moveVector;
    public int speed = 6;

    [HideInInspector]
    public Vector2 climbPosition;
    [HideInInspector]
    public Vector2 narrowPosition;

    public bool isFacing;

    public GameObject column;

    public GameObject crate;
    public bool stopRotation;

    bool stop;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = moveVector * speed;

        //questo fa schifo ed è temporaneo, quando ci saranno le animazioni si farà
        //il flip con quelle
        if (moveVector != Vector2.zero && !stopRotation)
        {
            float angle = Mathf.Atan2(moveVector.y, moveVector.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
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
        //ho messo il bool stop per fare in modo che se il giocatore è davanti a 
        //due possibili interazioni ne esegue soltanto una e deve interagire di nouvo
        //per fare l'altra
        //sono messe in ordine di priorità decrescente
        stop = false;
        //metto stop a false ogni volta che premo il tasto di interazione, così so che
        //ne posso fare una, alla fine di ogni interazione se è stata fatta
        //metto stop a true

        if(!stop)
        CrateInteract();
        if(!stop)
        Climb();
        if(!stop)
        PassThrough();
        if(!stop)
        BreakColumn();
    }

    void CrateInteract()
    {
        if(crate != null)
        {
            //stopRotation poi sarà per bloccare l'animazione sul personaggio che
            //spinge la cassa, per ora blocca la rotazione
            crate.GetComponent<Crate>().CrateInteraction(gameObject);
            stopRotation = !stopRotation;
            stop = true;
        }
    }

    void BreakColumn()
    {
        if (column != null && isFacing)
        {
            //animazione
            column.GetComponentInParent<Column>().BreakDownColumn();
            stop = true;
        }
    }

    void Climb()
    {
        //animazione del climb
        if (climbPosition != Vector2.zero && isFacing)
        {
            transform.position = climbPosition;
            stop = true;
        }
    }

    void PassThrough()
    {
        //animazione del passaggio
        if(narrowPosition != Vector2.zero && isFacing)
        {
            transform.position = narrowPosition;
            stop = true;
        }
    }
}
