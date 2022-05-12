using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public GameManager gameManager;
    [HideInInspector]
    public Rigidbody2D rb;
    [HideInInspector]
    public Vector2 moveVector;
    public int speed = 6;
    [HideInInspector]
    public bool isFacing;
    [HideInInspector]
    public bool stopRotation;
    [HideInInspector]
    public GameObject column;
    [HideInInspector]
    public GameObject crate;

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

    void OnInteract()
    {
        stop = false;
        if (!stop)
            CrateInteract();
        if (!stop)
            BreakColumn();
    }

    void CrateInteract()
    {
        if (crate != null)
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

    //ho spostato la logica dello swap nel gamemanager, mi pareva più sensato
    void OnSwap()
    {
        gameManager.Swap();
    }
}
