using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    bool childIn;
    bool adultIn;
    [SerializeField] float cameraLerpDelay;
    [SerializeField] float cameraLerpSlow;
    float timeElapsed;

    public bool changeScene;

    public Transform newCameraPosition;
    int newCameraPos;
    public Camera camera;
    bool moveCamera;

    public Transform newChildSpawn;
    public Transform newAdultSpawn;
    [SerializeField] NextLevel newNextLevel;

    Child child;
    Adult adult;
    [SerializeField] GameManager manager;

    public int level;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponentInParent<Child>())
        {
            childIn = true;
            child = collision.GetComponentInParent<Child>();
            collision.GetComponentInParent<Player>().interactableObject = gameObject;
        }     
        if(collision.GetComponentInParent<Adult>())
        {
            adultIn = true;
            adult = collision.GetComponentInParent<Adult>();
            collision.GetComponentInParent<Player>().interactableObject = gameObject;
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
            collision.GetComponentInParent<Player>().interactableObject = null;
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
            if(childIn && adultIn)
            {
                moveCamera = true;
                child.transform.position = newChildSpawn.position;
                adult.transform.position = newAdultSpawn.position;

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
        newChildSpawn.parent.gameObject.SetActive(true);
        newChildSpawn.gameObject.GetComponent<SpawnPoint>().SavePosition();
        newAdultSpawn.gameObject.GetComponent<SpawnPoint>().SavePosition();

        manager.childSpawnPos.parent.gameObject.SetActive(false);
        manager.childSpawnPos = newChildSpawn;
        manager.adultSpawnPos = newAdultSpawn;

        PlayerPrefs.SetInt("levelReached", level);

        if (child != null)
        {
            child.GetComponentInParent<Player>().interactableObject = null;
        }
    }

    //lerp della camera, quando arriva alla nuova posizione resetta i valori di controllo
    //attiva il prossimo NextLevel e si disattiva
    private void Update()
    {
        if (moveCamera)
        {
            Vector3 position = camera.transform.position;

            if (timeElapsed < cameraLerpDelay)
            {
                position.x = Mathf.Lerp(camera.transform.position.x, newCameraPosition.position.x, timeElapsed / (cameraLerpDelay*cameraLerpSlow));
                position.y = Mathf.Lerp(camera.transform.position.y, newCameraPosition.position.y, timeElapsed / (cameraLerpDelay*cameraLerpSlow));

                camera.transform.position = position;
                timeElapsed += Time.deltaTime;
            }
            else
            {
                timeElapsed = 0;
                moveCamera = false;
                newNextLevel.gameObject.SetActive(true);
                gameObject.SetActive(false);
            }
        }
    }
}
