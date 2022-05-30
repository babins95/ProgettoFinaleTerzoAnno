using System;
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

    //debug, da togliere poi
    public NextLevel nextLevel;

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
            DialogueInteraction();
        }
    }

    private void DialogueInteraction()
    {
        //se il gioco è in pausa non puoi interagire con l'npc
        if (interactableObject != null && interactableObject.GetComponent<NpcDialogue>() != null && !GameManager.pauseMenu.isActiveAndEnabled)
        {
            interactableObject.GetComponent<NpcDialogue>().DialogueInteraction();
        }
    }

    void GoNextLevel()
    {
        if (interactableObject != null && interactableObject.GetComponent<NextLevel>() != null)
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

    //zona debug, da eliminare in futuro
    //----------------------------------------------------------------------------//
    void OnDeleteSave()
    {
        PlayerPrefs.DeleteAll();
    }

    //vai avanti al prossimo checkpoint, fino all'ultimo della scena
    void OnSkip()
    {
        if (gameManager.currentLevel < gameManager.nextLevelGroup.transform.childCount - 1)
        {
            int skipLevel = gameManager.currentLevel + 1;
            PlayerPrefs.SetInt("saved", 1);
            PlayerPrefs.SetInt("levelReached", skipLevel);
            PlayerPrefs.SetFloat("xChild", gameManager.nextLevelGroup.transform.GetChild(skipLevel-1).GetChild(1).GetChild(0).position.x);
            PlayerPrefs.SetFloat("yChild", gameManager.nextLevelGroup.transform.GetChild(skipLevel-1).GetChild(1).GetChild(0).position.y);
            PlayerPrefs.SetFloat("xAdult", gameManager.nextLevelGroup.transform.GetChild(skipLevel-1).GetChild(1).GetChild(1).position.x);
            PlayerPrefs.SetFloat("yAdult", gameManager.nextLevelGroup.transform.GetChild(skipLevel-1).GetChild(1).GetChild(1).position.y);
            PlayerPrefs.Save();
            gameManager.ResetRoom();
        }
    }

    //vai indietro di un checkpoint, fino al primo della scena
    void OnBack()
    {
        if (gameManager.currentLevel >= 2)
        {
            int skipLevel = gameManager.currentLevel - 1;
            PlayerPrefs.SetInt("saved", 1);
            PlayerPrefs.SetInt("levelReached", skipLevel);
            PlayerPrefs.SetFloat("xChild", gameManager.nextLevelGroup.transform.GetChild(skipLevel-1).GetChild(1).GetChild(0).position.x);
            PlayerPrefs.SetFloat("yChild", gameManager.nextLevelGroup.transform.GetChild(skipLevel-1).GetChild(1).GetChild(0).position.y);
            PlayerPrefs.SetFloat("xAdult", gameManager.nextLevelGroup.transform.GetChild(skipLevel-1).GetChild(1).GetChild(1).position.x);
            PlayerPrefs.SetFloat("yAdult", gameManager.nextLevelGroup.transform.GetChild(skipLevel-1).GetChild(1).GetChild(1).position.y);
            PlayerPrefs.Save();
            gameManager.ResetRoom();
        }
        else if(gameManager.currentLevel == 1)
        {
            int skipLevel = gameManager.currentLevel - 1;
            PlayerPrefs.SetInt("saved", 1);
            PlayerPrefs.SetInt("levelReached", skipLevel);
            PlayerPrefs.SetFloat("xChild", gameManager.childSpawnPos.position.x);
            PlayerPrefs.SetFloat("yChild", gameManager.childSpawnPos.position.y);
            PlayerPrefs.SetFloat("xChild", gameManager.childSpawnPos.position.x);
            PlayerPrefs.SetFloat("xAdult", gameManager.adultSpawnPos.position.x);
            PlayerPrefs.SetFloat("yAdult", gameManager.adultSpawnPos.position.y);
            PlayerPrefs.Save();
            gameManager.ResetRoom();
        }
    }
}