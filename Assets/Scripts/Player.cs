using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 moveVector;
    public int speed = 6;

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
