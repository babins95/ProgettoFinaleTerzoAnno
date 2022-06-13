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
    public bool stopAnimation;
    [HideInInspector]
    public GameObject interactableObject;

    [SerializeField] float offset = 0.75f;

    Animator animator;
    Vector3 moveDirection;
    PlayerEye playerEye;
    PlayerBack playerBack;

    //debug, da togliere poi
    public NextLevel nextLevel;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerEye = GetComponentInChildren<PlayerEye>();
        playerBack = GetComponentInChildren<PlayerBack>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = moveVector * speed;

        if (!stopAnimation)
        {
            Animate(moveVector.x, moveVector.y);
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

    void Animate(float x, float y)
    {
        if(moveVector != Vector2.zero)
        {
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }

        moveDirection = new Vector3(x, y, 0);

        moveDirection = transform.InverseTransformDirection(moveDirection);

        animator.SetFloat("moveX", moveVector.x);
        animator.SetFloat("moveY", moveVector.y);

        MoveCollider(playerEye, playerBack);
    }

    void MoveCollider(PlayerEye eye, PlayerBack back)
    {
        if(animator.GetFloat("moveY") != 0)
        {
            eye.SetPosition(new Vector3(0, moveVector.y, 0));
            back.SetPosition(new Vector3(0, -moveVector.y, 0));
        }
        else if(animator.GetFloat("moveX") == 1)
        {
            eye.SetPosition(Vector3.zero);
            back.SetPosition(new Vector3(-offset, 0, 0));
        }
        else if(animator.GetFloat("moveX") == -1)
        {
            eye.SetPosition(new Vector3(-offset, 0, 0));
            back.SetPosition(Vector3.zero);
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