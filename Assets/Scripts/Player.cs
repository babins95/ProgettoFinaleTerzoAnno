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

    public Vector2 climbPosition;
    public Vector2 narrowPosition;


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
        //animazione del climb
        if (climbPosition != Vector2.zero)
        {
            transform.position = climbPosition;
        }
    }

    void PassThrough()
    {
        //animazione del passaggio
        if(narrowPosition != Vector2.zero)
        {
            transform.position = narrowPosition;
        }
    }
}
