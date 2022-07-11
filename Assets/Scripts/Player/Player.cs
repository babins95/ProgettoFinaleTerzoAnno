using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public GameManager gameManager;
    [HideInInspector]
    public Rigidbody2D rb;
    [HideInInspector]
    public Vector2 moveVector;
    public float speed = 6;
    [HideInInspector]
    public bool isFacing;
    [HideInInspector]
    public bool stopAnimation;
    [HideInInspector]
    public GameObject interactableObject;

    Animator animator;
    Vector3 moveDirection;
    PlayerEye playerEye;
    PlayerBack playerBack;
    public BulletSpawner bulletSpawner;

    [HideInInspector]
    public bool obstacleAhead;    
    [HideInInspector]
    public int eyePosCounter;
    ObstacleCheck obstacleCheck;
    //debug, da togliere poi
    public NextLevel nextLevel;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerEye = GetComponentInChildren<PlayerEye>();
        playerBack = GetComponentInChildren<PlayerBack>();
        obstacleCheck = GetComponentInChildren<ObstacleCheck>();
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

    void Animate(float x, float y)
    {
        if (moveVector != Vector2.zero)
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

        MoveCollider();
    }

    void MoveCollider()
    {
        if (animator.GetFloat("moveY") == 1)
        {
            playerBack.transform.localPosition = new Vector3(0, playerBack.posY, 0);
            playerEye.transform.localPosition = new Vector3(0, playerEye.posY, 0);
            obstacleCheck.transform.localPosition = new Vector3(0, obstacleCheck.posY, 0);
            if (bulletSpawner != null)
            {
                bulletSpawner.transform.localPosition = new Vector3(0, playerEye.posY, 0);
                bulletSpawner.bulletDirection = Vector2.up;
            }
            eyePosCounter = 1;
            //up
            animator.SetFloat("direction", 1);
        }
        else if (animator.GetFloat("moveY") == -1)
        {
            playerBack.transform.localPosition = new Vector3(0, -playerBack.posY, 0);
            playerEye.transform.localPosition = new Vector3(0, -playerEye.posY, 0);
            obstacleCheck.transform.localPosition = new Vector3(0, -obstacleCheck.posY, 0);
            if (bulletSpawner != null)
            {
                bulletSpawner.transform.localPosition = new Vector3(0, -playerEye.posY, 0);
                bulletSpawner.bulletDirection = Vector2.down;
            }
            eyePosCounter = 2;
            //down
            animator.SetFloat("direction", 0);
        }
        else if (animator.GetFloat("moveX") == 1)
        {
            playerBack.transform.localPosition = new Vector3(playerBack.posX, 0, 0);
            playerEye.transform.localPosition = new Vector3(playerEye.posX, 0, 0);
            obstacleCheck.transform.localPosition = new Vector3(obstacleCheck.posX, 0, 0);
            if (bulletSpawner != null)
            {
                bulletSpawner.transform.localPosition = new Vector3(bulletSpawner.posX, bulletSpawner.posY, 0);
                bulletSpawner.bulletDirection = Vector2.right;
            }
            eyePosCounter = 3;
            //right
            animator.SetFloat("direction", 0.7f);
        }
        else if (animator.GetFloat("moveX") == -1)
        {
            playerBack.transform.localPosition = new Vector3(-playerBack.posX, 0, 0);
            playerEye.transform.localPosition = new Vector3(-playerEye.posX, 0, 0);
            obstacleCheck.transform.localPosition = new Vector3(-obstacleCheck.posX, 0, 0);
            if (bulletSpawner != null)
            {
                bulletSpawner.transform.localPosition = new Vector3(-bulletSpawner.posX, bulletSpawner.posY, 0);
                bulletSpawner.bulletDirection = Vector2.left;
            }
            eyePosCounter = 4;
            //left
            animator.SetFloat("direction", 0.3f);
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
            PlayerPrefs.SetFloat("xChild", gameManager.nextLevelGroup.transform.GetChild(skipLevel - 1).GetChild(1).GetChild(0).position.x);
            PlayerPrefs.SetFloat("yChild", gameManager.nextLevelGroup.transform.GetChild(skipLevel - 1).GetChild(1).GetChild(0).position.y);
            PlayerPrefs.SetFloat("xAdult", gameManager.nextLevelGroup.transform.GetChild(skipLevel - 1).GetChild(1).GetChild(1).position.x);
            PlayerPrefs.SetFloat("yAdult", gameManager.nextLevelGroup.transform.GetChild(skipLevel - 1).GetChild(1).GetChild(1).position.y);
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
            PlayerPrefs.SetFloat("xChild", gameManager.nextLevelGroup.transform.GetChild(skipLevel - 1).GetChild(1).GetChild(0).position.x);
            PlayerPrefs.SetFloat("yChild", gameManager.nextLevelGroup.transform.GetChild(skipLevel - 1).GetChild(1).GetChild(0).position.y);
            PlayerPrefs.SetFloat("xAdult", gameManager.nextLevelGroup.transform.GetChild(skipLevel - 1).GetChild(1).GetChild(1).position.x);
            PlayerPrefs.SetFloat("yAdult", gameManager.nextLevelGroup.transform.GetChild(skipLevel - 1).GetChild(1).GetChild(1).position.y);
            PlayerPrefs.Save();
            gameManager.ResetRoom();
        }
        else if (gameManager.currentLevel == 1)
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