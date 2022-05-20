using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static Canvas pauseMenu;
    //di base � nella versione giovane, quindi se swap = true ha cambiato ad adolescente
    public static bool swap = false;
    public Transform childSpawnPos;
    public Transform adultSpwanPos;

    //scale di default del giocatore e scale attuale del giocatore
    Vector3 defaultPlayerScale;
    Vector3 playerScale;
    //scale del giocatore alla fine della caduta
    float scaleTarget = 0.5f;

    [HideInInspector]
    public bool falling;
    [SerializeField] int fallTimer = 5;

    //public SwitchCounter SwitchCounter;
    public GameObject child;
    public GameObject adult;

    public float trasparenza;
    Scene currentScene;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        defaultPlayerScale = child.transform.localScale;
        playerScale.x= child.transform.localScale.x;
        playerScale.y= child.transform.localScale.y;

        pauseMenu = PauseMenu.thisPauseMenu;
        //se � presente un file di salvataggio
        if (PlayerPrefs.GetInt("saved") == 1)
        {
            //setta la posizione del giocatore a quella dell'ultimo checkpoint toccato
            Spawn();
        }
        else
        {
            //altrimenti parti dalla posizione di inizio gioco
            child.transform.position = childSpawnPos.position;
            adult.transform.position = adultSpwanPos.position;
        }

        TurnOff(adult);
    }

    void OnPause()
    {
        //se premi il pulsante di pausa il tempo si ferma e il gameobject si attiva altrimenti succede il contrario
        if(Time.timeScale == 0)
        {
            Time.timeScale = 1;
            pauseMenu.gameObject.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            pauseMenu.gameObject.SetActive(true);
        }
    }

    public void Spawn()
    {
        float childX = PlayerPrefs.GetFloat("xChild");
        float childY = PlayerPrefs.GetFloat("yChild");
        float adultX = PlayerPrefs.GetFloat("xAdult");
        float adultY = PlayerPrefs.GetFloat("yAdult");

        child.transform.position = new Vector3(childX, childY, 0);
        adult.transform.position = new Vector3(adultX, adultY, 0);
        
    }


    private void Update()
    {
        if(falling)
        {
            if(fallTimer > 0)
            {
                fallTimer--;
            }
            else
            {
                //se stai cadendo interrompo il movimento e disattivo l'actionmap
                //togliendo al giocatore la possibilit� di agire
                adult.GetComponent<Player>().moveVector = Vector2.zero;
                adult.GetComponent<Player>().GetComponent<PlayerInput>().DeactivateInput();
                //e diminuisco i valore della scale fino ad arrivare alla dimensione
                //da fine caduta
                if (playerScale.x > scaleTarget)
                {
                    playerScale.x -= 0.01f;
                    playerScale.y -= 0.01f;

                    adult.transform.localScale = new Vector3(playerScale.x, playerScale.y, 1);
                    fallTimer = 5;
                }
                else
                {
                    ResetRoom();
                }
            }
        }
    }

    //oltre a quello che gi� faceva in player lo swap ora disattiva controlli e collisione
    //della parte non attiva e la mette trasparente
    //tolgo anche il playerEye della versione spenta
    public void Swap()
    {
        swap = !swap;
        //SwitchCounter.SwitchUsed();
        if (swap)
        {
            //vecchio
            if (adult.GetComponent<Player>().interactableObject != null)
            {
                if (adult.GetComponent<Player>().interactableObject.GetComponent<Crate>() != null)
                {
                    PutDownCrate(adult);
                }
            }
            TurnOff(child);
            TurnOn(adult);
        }
        else if (swap == false)
        {
            //giovane
            if (adult.GetComponent<Player>().interactableObject != null)
            {
                if (adult.GetComponent<Player>().interactableObject.GetComponent<Crate>() != null)
                {
                    PutDownCrate(adult);
                }
            }
            TurnOff(adult);
            TurnOn(child);
        }
    }

    void TurnOff(GameObject toTurnOff)
    {
        toTurnOff.GetComponent<Player>().moveVector = Vector2.zero;
        toTurnOff.GetComponent<PlayerInput>().enabled = false;
        toTurnOff.GetComponent<BoxCollider2D>().enabled = false;
        Color newColor = toTurnOff.GetComponent<SpriteRenderer>().color;
        newColor.a = trasparenza;
        toTurnOff.GetComponent<SpriteRenderer>().color = newColor;
        toTurnOff.transform.GetChild(0).gameObject.SetActive(false);
    }

    void TurnOn(GameObject toTurnOn)
    {
        toTurnOn.GetComponent<PlayerInput>().enabled = true;
        toTurnOn.GetComponent<BoxCollider2D>().enabled = true;
        Color newColor = toTurnOn.GetComponent<SpriteRenderer>().color;
        newColor.a = 1f;
        toTurnOn.GetComponent<SpriteRenderer>().color = newColor;
        toTurnOn.transform.GetChild(0).gameObject.SetActive(true);
    }

    void PutDownCrate(GameObject puttingDown)
    {
        puttingDown.GetComponent<Player>().interactableObject.GetComponent<Crate>().CrateInteraction(puttingDown);
        puttingDown.GetComponent<Player>().stopRotation = false;
    }

    void ResetRoom()
    {
        swap = false;
        SceneManager.LoadScene(currentScene.name);
    }
}
