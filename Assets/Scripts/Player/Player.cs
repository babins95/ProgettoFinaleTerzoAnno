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
    public GameObject interactableObject;

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
        if (interactableObject != null)
        {
            BreakColumn();
            GoNextLevel();
        }
    }

    void GoNextLevel()
    {
        if(interactableObject.GetComponent<NextLevel>() != null)
        {
            interactableObject.GetComponent<NextLevel>().GoNextLevel();
        }
    }

    void BreakColumn()
    {
        if (interactableObject.GetComponentInParent<Column>() != null && isFacing)
        {
            //animazione
            interactableObject.GetComponentInParent<Column>().BreakDownColumn();
        }
    }

    //ho spostato la logica dello swap nel gamemanager, mi pareva più sensato
    void OnSwap()
    {
        gameManager.Swap();
    }
}
