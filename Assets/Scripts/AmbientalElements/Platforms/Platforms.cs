using UnityEngine;

public class Platforms : MonoBehaviour
{
    public static bool onPlatform;
    private LayerMask collisionLayer;
    private bool swap;

    //Quando il player entra a contatto con la piattaforma cambio il suo layer in modo che non senta il collider della tilemap,
    //Quando esce resetto tutto

    private void Start()
    {
        collisionLayer = 8;
        swap = GameManager.swap;
    }
    private void Update()
    {
        if(GameManager.swap != swap)
        {
            swap = GameManager.swap;
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            collision.gameObject.layer = gameObject.layer;
            foreach(Transform child in collision.transform)
            {
                if (!child.gameObject.GetComponent<ObstacleCheck>())
                {
                    child.gameObject.layer = gameObject.layer;
                }
            }
            collision.transform.parent = gameObject.transform.GetChild(0).transform;
            collision.transform.rotation = Quaternion.Euler(Vector3.zero);
            onPlatform = true;
        }
    }
    //all'uscita tolgo la parentela e resetto la rotazione
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            collision.gameObject.layer = collisionLayer;
            foreach (Transform child in collision.transform)
            {
                if (!child.gameObject.GetComponent<ObstacleCheck>())
                {
                    child.gameObject.layer = collisionLayer;
                }
            }
            collision.transform.parent = null;
            collision.transform.rotation = Quaternion.Euler(Vector3.zero);
            onPlatform = false;
        }
    }
}
