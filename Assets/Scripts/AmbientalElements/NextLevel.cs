using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    bool childIn;
    bool adultIn;
    [SerializeField] float cameraLerpDuration;
    float timeElapsed;

    public bool changeScene;

    public Transform newCameraPosition;
    int newCameraPos;
    public Camera camera;
    public float cameraSpeed;
    bool moveCamera;

    public Transform newChildSpawn;
    public Transform newAdultSpawn;

    Child child;
    Adult adult;

    [SerializeField] GameManager manager;
    [SerializeField] NextLevel newNextLevel;

    private void Start()
    {
        newCameraPos = PlayerPrefs.GetInt("newCameraPos");
        
        if(newCameraPos == 1)
        {
            camera.transform.position = newCameraPosition.position;
            ChangeSpawnPoint();
        }
    }

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
                PlayerPrefs.SetInt("newCameraPos", 0);
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

                PlayerPrefs.SetInt("newCameraPos", 1);
            }
        }
    }

    private void ChangeSpawnPoint()
    {
        newChildSpawn.parent.gameObject.SetActive(true);
        newChildSpawn.gameObject.GetComponent<SpawnPoint>().SavePosition();
        newAdultSpawn.gameObject.GetComponent<SpawnPoint>().SavePosition();

        manager.childSpawnPos.parent.gameObject.SetActive(false);
        manager.childSpawnPos = newChildSpawn;
        manager.adultSpawnPos = newAdultSpawn;

        if (child != null)
        {
            child.GetComponentInParent<Player>().interactableObject = null;
        }
    }

    private void Update()
    {
        if (moveCamera)
        {
            //float interp = cameraSpeed * Time.deltaTime;

            Vector3 position = camera.transform.position;
            //position.x = Mathf.Lerp(camera.transform.position.x, newCameraPosition.position.x, interp);
            //position.y = Mathf.Lerp(camera.transform.position.y, newCameraPosition.position.y, interp);
            //camera.transform.position = position;

            if (timeElapsed < cameraLerpDuration)
            {
                position.x = Mathf.Lerp(camera.transform.position.x, newCameraPosition.position.x, timeElapsed / cameraLerpDuration);
                position.y = Mathf.Lerp(camera.transform.position.y, newCameraPosition.position.y, timeElapsed / cameraLerpDuration);

                camera.transform.position = position;
                timeElapsed += Time.deltaTime;
            }
            else
            {
                moveCamera = false;
                this.gameObject.SetActive(false);
                newNextLevel.gameObject.SetActive(true);
            }
        }

        //if(camera.transform.position == newCameraPosition.position)
        //{
        //    moveCamera = false;
        //    this.gameObject.SetActive(false);
        //}
    }
}
