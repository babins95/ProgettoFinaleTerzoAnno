using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class NextLevel : MonoBehaviour
{
    bool childIn;
    bool adultIn;
    public bool changeScene;

    public Transform newChildSpawn;
    public Transform newAdultSpawn;

    Child child;
    Adult adult;

    [SerializeField] CinemachineVirtualCamera nextVCam;

    int levelReached;

    private void Start()
    {
        levelReached = PlayerPrefs.GetInt("levelReached");

        if (levelReached > 0)
        {
            //a seconda di quale livello hai raggiunto attiva il corrispettivo prefab nel gruppo dei NextLevel
            gameObject.transform.parent.transform.parent.GetChild(levelReached).gameObject.SetActive(true);
            //ma spenge la NewCameraPosition, visto che si attiverà solo al passaggio di livello
            gameObject.transform.parent.transform.parent.GetChild(levelReached).gameObject.transform.GetChild(2).gameObject.SetActive(false);
            //attiva il livello precedente a quello attuale per avere la camera corrente 
            gameObject.transform.parent.transform.parent.GetChild(levelReached - 1).gameObject.SetActive(true);
        }

        ChangeCamera(levelReached);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.GetComponentInParent<Child>())
        {
            childIn = true;
            child = collision.GetComponentInParent<Child>();
            collision.GetComponentInParent<Player>().interactableObject = gameObject;
        }
        if (collision.GetComponentInParent<Adult>())
        {
            adultIn = true;
            adult = collision.GetComponentInParent<Adult>();
            if (collision.GetComponentInParent<Adult>().hasCrate == false)
            {
                collision.GetComponentInParent<Player>().interactableObject = gameObject;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<Child>())
        {
            //se sei ancora in fase child childIn diventa falso perchè ti sei mosso
            //se parte OnTriggerExit perchè hai fatto lo swap all'adulto rimane vero
            if (GameManager.swap == false)
            {
                childIn = false;
            }
            collision.GetComponentInParent<Player>().interactableObject = null;
        }
        if (collision.GetComponentInParent<Adult>())
        {
            //idem al contrario se sei adult
            if (GameManager.swap == true)
            {
                adultIn = false;
            }
            if (!collision.GetComponentInParent<Animator>().GetBool("hasBox") && !collision.GetComponentInParent<Animator>().GetBool("hasMirrorBox"))
            {
                collision.GetComponentInParent<Player>().interactableObject = null;
            }
        }
    }

    public void GoNextLevel()
    {
        if (changeScene)
        {
            //se entrambi i giocatori sono sul trigger l'interazione ti manda al prossimo livello
            if (childIn && adultIn)
            {
                //eventuale animazione? non so come lo vogliono fare di preciso
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
        else
        {
            if (childIn && adultIn)
            {
                if(child.GetComponentInParent<Player>().gameManager.mainCamera.GetComponent<CinemachineBrain>().m_DefaultBlend.m_Time == 0f)
                {
                    child.GetComponentInParent<Player>().gameManager.mainCamera.GetComponent<CinemachineBrain>().m_DefaultBlend.m_Time = 2f;
                }

                //attiva il prossimo prefab NextLevel
                gameObject.transform.parent.transform.parent.GetChild(levelReached + 1).gameObject.SetActive(true);
                //e la NewCameraPosition legata al livello attuale
                gameObject.transform.parent.GetChild(2).gameObject.SetActive(true);

                ChangeCamera(levelReached + 1);
                child.transform.position = newChildSpawn.position;
                adult.transform.position = newAdultSpawn.position;

                PlayerPrefs.SetInt("levelReached", levelReached + 1);
                PlayerPrefs.Save();
                ChangeSpawnPoint();
            }
        }
    }

    //cambia lo spawnPoint del giocatore e tutti i suoi riferimenti al nuovo checkpoint raggiunto
    //il levelReached è come gli stage di super mario, le scene i mondi
    //per esempio per salvare la posizione del giocatore ci sarà:
    //PlayerPrefs.GetString("lastScene") dove lastScene è Livello2
    //PlayerPrefs.GetInt("levelReached") dove levelReached è 3
    //quando apri il gioco ti carica la scena Livello2 e ti setta lo spawn alla posizione del
    //terzo spawnPoint; livello 2-3
    private void ChangeSpawnPoint()
    {
        newChildSpawn.gameObject.GetComponent<SpawnPoint>().SavePosition();
        newAdultSpawn.gameObject.GetComponent<SpawnPoint>().SavePosition();

        PlayerPrefs.SetInt("levelReached", levelReached + 1);

        if (child != null)
        {
            child.GetComponentInParent<Player>().interactableObject = null;
        }
    }

    void ChangeCamera(int currentCamera)
    {
        nextVCam.Priority = currentCamera;
    }
}
