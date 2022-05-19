using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public bool childIn;
    public bool adultIn;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponentInParent<Child>())
        {
            childIn = true;
            collision.GetComponentInParent<Player>().interactableObject = gameObject;
        }     
        if(collision.GetComponentInParent<Adult>())
        {
            adultIn = true;
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
        //se entrambi i giocatori sono sul trigger l'interazione ti manda al prossimo livello
        if(childIn && adultIn)
        {
            //eventuale animazione? non so come lo vogliono fare di preciso
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
